using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    [Route("carrinho")]
    public class CarrinhoController : MainController
    {
        private readonly IComprasBffService _comprasBffSerivce;

        public CarrinhoController(IComprasBffService comprasService)
        {
            _comprasBffSerivce = comprasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _comprasBffSerivce.GetCarrinhoAsync());

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel itemProduto)
        {
            var response = await _comprasBffSerivce.AdicionarItemCarrinhoAsync(itemProduto);

            if (ResponsePossuiErros(response)) return View("Index", await _comprasBffSerivce.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }

        [HttpPost("atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var item = new ItemCarrinhoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
            var response = await _comprasBffSerivce.AtualizarItemCarrinhoAsync(produtoId, item);

            if (ResponsePossuiErros(response)) return View("Index", await _comprasBffSerivce.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }

        [HttpPost("excluir-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _comprasBffSerivce.RemoverItemCarrinhoAsync(produtoId);

            if (ResponsePossuiErros(response)) return View("Index", await _comprasBffSerivce.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }

        [HttpPost("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var response = await _comprasBffSerivce.AplicarvoucherCarrinhoAsync(voucherCodigo);

            if (ResponsePossuiErros(response)) return View("Index", await _comprasBffSerivce.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }
    }
}