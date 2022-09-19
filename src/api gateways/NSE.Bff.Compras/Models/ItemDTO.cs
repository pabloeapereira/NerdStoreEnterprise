namespace NSE.Bff.Compras.Models
{
    public abstract class ItemDTO 
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
    }
}