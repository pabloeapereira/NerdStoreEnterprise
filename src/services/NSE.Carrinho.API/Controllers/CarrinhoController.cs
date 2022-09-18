using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Carrinho.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    [Route("carrinhos")]
    public sealed class CarrinhoController:MainController
    {
        private readonly IAspNetUser _user;

        public CarrinhoController(IAspNetUser user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<CarrinhoCliente> GetCarrinho()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem item)
        {
            return CustomResponse();
        }

        [HttpPut("{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            return CustomResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            return CustomResponse();
        }
    }
}