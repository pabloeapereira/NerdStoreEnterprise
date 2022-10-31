using NSE.Core.DomainObjects;
using NSE.Pedidos.Domain.Vouchers;

namespace NSE.Pedidos.Domain.Pedidos
{
    public class Pedido : Entity, IAggregateRoot
    {
        public Pedido(Guid clienteId, decimal valorTotal, IList<PedidoItem> pedidoItens,bool voucherUtilizado = false, decimal desconto = decimal.Zero,  
            Guid? voucherId = null )
        {
            ClienteId = clienteId;
            VoucherId = voucherId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
        }

        public Pedido(){}
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItens = Enumerable.Empty<PedidoItem>().ToList();
        public IReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens;

        public Endereco Endereco { get; private set; }

        public Voucher Voucher { get; private set; }

        public void AutorizarPedido() => PedidoStatus = PedidoStatus.Autorizado;

        public void SetVoucher(Voucher voucher)
        {
            VoucherUtilizado = true;
            VoucherId = voucher.Id;
            Voucher = voucher;
        }

        public void SetEndereco(Endereco endereco) => Endereco = endereco;

        private void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            var desconto = decimal.Zero;
            var valor = ValorTotal;

            if (Voucher.TipoDesconto == TipoDescontoVoucher.Porcentagem && Voucher.Percentual.HasValue)
            {
                desconto = (valor * Voucher.Percentual.Value);
                valor -= desconto;
            }
            else if (Voucher.ValorDesconto.HasValue)
            {
                desconto = Voucher.ValorDesconto.Value;
                valor -= desconto;
            }

            ValorTotal = valor < decimal.Zero ? decimal.Zero : valor;
            Desconto = desconto;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItens.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }
    }
}