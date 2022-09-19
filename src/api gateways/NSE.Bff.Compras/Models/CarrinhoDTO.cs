namespace NSE.Bff.Compras.Models
{
    public class CarrinhoDTO
    {
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }
        public ICollection<ItemCarrinhoDTO> Itens { get; set; } = new List<ItemCarrinhoDTO>();
        public int QuantidadeItens => Itens.Count;
    }
}