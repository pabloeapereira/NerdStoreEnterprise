using FluentValidation;
using NSE.Core.Messages;
using NSE.Core.Validations;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedido.API.Application.Commands
{
    public class AdicionarPedidoCommand:Command
    {
        //Pedido
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public IEnumerable<PedidoItemDTO> PedidoItens { get; set; } = new List<PedidoItemDTO>();

        //Voucher
        public decimal Desconto { get; set; }
        public string VoucherCodigo { get; set; }
        public bool VoucherUtilizado { get; set; }

        //Endereco
        public EnderecoDTO Endereco { get; set; }
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public PedidoStatus Status { get; set; }
        public DateTime Data { get; set; }
        
        
        //Cartao
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }


        private sealed class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
        {
            public AdicionarPedidoValidation()
            {
                RuleFor(x => x.ClienteId).NotEqual(Guid.Empty).WithMessage("Id do cliente inválido");

                RuleFor(x => x.PedidoItens.Any()).Equal(true).WithMessage("O pedido precisa ter no mínimo 1 item");

                RuleFor(x => x.ValorTotal).GreaterThan(decimal.Zero).WithMessage("Valor do pedido inválido");

                RuleFor(x => x.NumeroCartao).CreditCard().WithMessage("Número do cartão inválido");

                RuleFor(x => x.NomeCartao).NotEmpty().WithMessage("Nome do portador do cartão requerido");

                RuleFor(x => x.CvvCartao.Length).GreaterThan(2).LessThan(5)
                    .WithMessage("O CVV do cartão precisa ter 3 ou 4 números");

                RuleFor(x => x.ExpiracaoCartao).NotEmpty().WithMessage("Data expiração do cartão requirida");
            }
        }

    }
}