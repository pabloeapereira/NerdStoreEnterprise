namespace NSE.WebApp.MVC.Models
{
    public class PedidoTransacaoViewModel
    {
        public decimal Pedido { get; set; }
        public decimal Desconto { get; set; }
        public bool VoucherUtilizado { get; set; }
        public string? VoucherCodigo { get; set; }
        public ICollection<ItemCarrinhoViewModel> Itens { get; set; }

        public EnderecoViewModel Endereco { get; set; }
    }
}