using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    public sealed class PedidoController:MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IComprasBffService _comprasBffService;

        public PedidoController(IClienteService clienteService, IComprasBffService comprasBffService)
        {
            _clienteService = clienteService;
            _comprasBffService = comprasBffService;
        }

        [HttpGet("endereco-de-entrega")]
        public async Task<IActionResult> EnderecoEntrega()
        {
            var carrinho = await _comprasBffService.GetCarrinhoAsync();
            if (!carrinho.Itens.Any()) return RedirectToAction("Index", "Carrinho");

            var endereco = await _clienteService.GetEnderecoAsync();

            var pedido = _comprasBffService.MapearParaPedido(carrinho, endereco);

            return View(pedido);
        }
    }
}