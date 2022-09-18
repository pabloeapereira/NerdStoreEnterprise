using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NSE.WebApp.MVC.Controllers
{
    [Route("carrinho")]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;

        public CarrinhoController(ICarrinhoService carrinhoService, ICatalogoService catalogoService)
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _carrinhoService.GetCarrinhoAsync());

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemProdutoViewModel itemProduto)
        {
            var produto = await _catalogoService.GetByIdAsync(itemProduto.ProdutoId);
            ValidarItemCarrinho(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return View("Index", await _carrinhoService.GetCarrinhoAsync());

            itemProduto.Nome = produto.Nome;
            itemProduto.Valor = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            var response = await _carrinhoService.AdicionarItemCarrinhoAsync(itemProduto);

            if (ResponsePossuiErros(response)) return View("Index", await _carrinhoService.GetCarrinhoAsync());



            return RedirectToAction("Index");
        }

        [HttpPost("atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var produto = await _catalogoService.GetByIdAsync(produtoId);
            ValidarItemCarrinho(produto, quantidade);
            if (!OperacaoValida()) return View("Index", await _carrinhoService.GetCarrinhoAsync());


            var itemProduto = new ItemProdutoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
            var response = await _carrinhoService.AtualizarItemCarrinhoAsync(produtoId, itemProduto);

            if (ResponsePossuiErros(response)) return View("Index", await _carrinhoService.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }

        [HttpPost("excluir-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.GetByIdAsync(produtoId);
            if (produto is null) 
            {
                AddError("Produto inexistente!");
                return View(await _carrinhoService.GetCarrinhoAsync());
            }

            var response = await _carrinhoService.RemoverItemCarrinhoAsync(produtoId);

            if (ResponsePossuiErros(response)) return View("Index", await _carrinhoService.GetCarrinhoAsync());

            return RedirectToAction("Index");
        }

        private void ValidarItemCarrinho(ProdutoViewModel produto, int quantidade)
        {
            if (produto == null) AddError("Produto inexistente!");
            if (quantidade < 1) AddError($"Escolha ao menos uma unidade do produto {produto.Nome}");
            if (quantidade > produto.QuantidadeEstoque) AddError($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
        }
    }
}