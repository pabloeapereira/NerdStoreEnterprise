using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Compras.Models;
using NSE.Bff.Compras.Services;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Compras.Controllers
{
    [Authorize]
    [Route("compras")]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IPedidoService _pedidoService;

        public CarrinhoController(ICarrinhoService carrinhoService, ICatalogoService catalogoService, IPedidoService pedidoService)
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
            _pedidoService = pedidoService;
        }

        [HttpGet("carrinho")]
        public async Task<IActionResult> Index() =>
            CustomResponse(await _carrinhoService.GetCarrinhoAsync());

        [HttpGet("carrinho-quantidade")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var quantidade = await _carrinhoService.GetCarrinhoAsync();
            return quantidade?.Itens.Sum(i => i.Quantidade) ?? 0;
        }

        [HttpPost("carrinho/itens")]
        public async Task<IActionResult> AdicionarItemCarrinho( ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.GetByIdAsync(itemProduto.ProdutoId);

            await ValidarItemCarrinhoAsync(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return CustomResponse();

            itemProduto.Nome = produto.Nome;
            itemProduto.Valor = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            return CustomResponse(await _carrinhoService.AdicionarItemCarrinhoAsync(itemProduto));
        }

        [HttpPut("carrinho/itens/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.GetByIdAsync(produtoId);

            await ValidarItemCarrinhoAsync(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return CustomResponse();

            return CustomResponse(await _carrinhoService.AtualizarItemCarrinhoAsync(produtoId,itemProduto));
        }

        [HttpDelete("carrinho/itens/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.GetByIdAsync(produtoId);
            if(produto is null)
            {
                AddError("Produto inexistente!");
                return CustomResponse();
            }

            return CustomResponse(await _carrinhoService.RemoverItemCarrinhoAsync(produtoId));
        }

        [HttpPost("carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await _pedidoService.GetVoucherByCodigoAsync(voucherCodigo);
            if (voucher is null)
            {
                AddError("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var response = await _carrinhoService.AplicarVoucherCarrinhoAsync(voucher);
            return CustomResponse(response);
        }

        private async Task ValidarItemCarrinhoAsync(ItemProdutoDTO produto, int quantidade)
        {
            if (produto is null) AddError("Produto inexistente!");
            if (quantidade < 1) AddError($"Escolha ao menos uma unidade do produto {produto.Nome}");

            var carrinho = await _carrinhoService.GetCarrinhoAsync();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(p => p.ProdutoId == produto.Id);

            if (itemCarrinho is not null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
            {
                AddError($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
                return;
            }

            if (quantidade > produto.QuantidadeEstoque)
                AddError($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
        }
    }
}