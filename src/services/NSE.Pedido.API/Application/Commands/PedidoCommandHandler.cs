using AutoMapper;
using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pedido.API.Application.Events;
using NSE.Pedidos.Domain.Pedidos;
using NSE.Pedidos.Domain.Vouchers;
using NSE.Pedidos.Domain.Vouchers.Specs;

namespace NSE.Pedido.API.Application.Commands
{
    public class PedidoCommandHandler : CommandHandler, IRequestHandler<AdicionarPedidoCommand,ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMessageBus _bus;

        public PedidoCommandHandler(IMapper mapper, IVoucherRepository voucherRepository, IPedidoRepository pedidoRepository, IMessageBus bus)
        {
            _mapper = mapper;
            _voucherRepository = voucherRepository;
            _pedidoRepository = pedidoRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedido = PedidoMapper(message);

            if (!await AplicarVoucherAsync(message, pedido)) return ValidationResult;

            if (!ValidarPedido(pedido)) return ValidationResult;

            if (!await ProcessarPagamentoAsync(pedido,message)) return ValidationResult;

            pedido.AutorizarPedido();

            pedido.AddEvent(new PedidoRealizadoEvent(pedido.Id,pedido.ClienteId));

            await _pedidoRepository.AddAsync(pedido);

            return await CommitAsync(_pedidoRepository.UnitOfWork);
        }

        private Pedidos.Domain.Pedidos.Pedido PedidoMapper(AdicionarPedidoCommand message)
        {
            var endereco = new Endereco
            {
                Logradouro =message.Endereco.Logradouro,
                Numero = message.Endereco.Numero,
                Complemento = message.Endereco.Complemento,
                Bairro = message.Endereco.Bairro,
                Cep = message.Endereco.Cep,
                Cidade = message.Endereco.Cidade,
                Estado = message.Endereco.Estado
            };

            var pedido = new Pedidos.Domain.Pedidos.Pedido(message.ClienteId, message.ValorTotal,
                _mapper.Map<List<PedidoItem>>(message.PedidoItens),
                message.VoucherUtilizado, message.Desconto);

            pedido.SetEndereco(endereco);
            return pedido;
        }

        private async ValueTask<bool> AplicarVoucherAsync(AdicionarPedidoCommand message,
            Pedidos.Domain.Pedidos.Pedido pedido)
        {
            if (!message.VoucherUtilizado) return true;

            var voucher = await _voucherRepository.GetByCodigoAsync(message.VoucherCodigo);
            if (voucher is null)
            {
                AddError("O voucher informado não existe!");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ForEach(m => AddError(m.ErrorMessage));
                return false;
            }

            pedido.SetVoucher(voucher);
            voucher.DebitarQuantidade();

            _voucherRepository.Update(voucher);

            return true;
        }

        private bool ValidarPedido(Pedidos.Domain.Pedidos.Pedido pedido)
        {
            var pedidoValorOriginal = pedido.ValorTotal;
            var pedidoDesconto = pedido.Desconto;

            pedido.CalcularValorPedido();

            if (pedido.ValorTotal != pedidoValorOriginal)
            {
                AddError("O valor total do pedido não confere com o cálculo do pedido");
                return false;
            }

            if (pedido.Desconto != pedidoDesconto)
            {
                AddError("O valor total não confere com o cálculo do pedido");
                return false;
            }

            return true;
        }

        public async Task<bool> ProcessarPagamentoAsync(Pedidos.Domain.Pedidos.Pedido pedido, AdicionarPedidoCommand message)
        {
            var pedidoIniciado = new PedidoIniciadoIntegrationEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal,
                    message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao);

            var result = await _bus.RequestAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(pedidoIniciado);

            AddErrors(result.ValidationResult);

            return result.ValidationResult.IsValid;
        }
    }
}