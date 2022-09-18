namespace NSE.WebApp.MVC.Models
{
    public class CarrinhoViewModel
    {
        public decimal ValorTotal { get; set; }
        public ICollection<ItemProdutoViewModel> Itens { get; set; } = new List<ItemProdutoViewModel>();
    }
}
