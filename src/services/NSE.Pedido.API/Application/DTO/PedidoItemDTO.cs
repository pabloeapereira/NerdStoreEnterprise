namespace NSE.Pedido.API.Application.DTO
{
    public sealed class PedidoItemDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string Imagem { get; set; }
    }
}
