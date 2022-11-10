using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedido.API.Application.DTO
{
    public sealed class PedidoDTO
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public PedidoStatus Status { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }
        public string VoucherCodigo { get; set; }
        public bool VoucherUtilizado { get; set; }

        public IList<PedidoItemDTO> PedidoItens { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}
