using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Compras.Controllers
{
    [Authorize]
    [Route("compras")]
    public class CarrinhoController : MainController
    {
        [HttpGet("carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet("carrinho-quantidade")]
        public async Task<IActionResult> ObterQuantidadeCarrinho()
        {
            return CustomResponse();
        }

        [HttpPost("carrinho/itens")]
        public async Task<IActionResult> AdicionarItemCarrinho()
        {
            return CustomResponse();
        }

        [HttpPut("carrinho/itens/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId)
        {
            return CustomResponse();
        }

        [HttpDelete("carrinho/itens/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            return CustomResponse();
        }
    }
}