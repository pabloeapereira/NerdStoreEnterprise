using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Data.Repository;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Clientes.API.Controllers
{
    [Route("clientes")]
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IClienteRepository _clienteRepository;
        private readonly IAspNetUser _user;

        public ClientesController(IMediatorHandler mediatorHandler, IClienteRepository clienteRepository, IAspNetUser user)
        {
            _mediatorHandler = mediatorHandler;
            _clienteRepository = clienteRepository;
            _user = user;
        }

        [HttpGet("endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.GetEnderecoByClientIdAsync(_user.ObterUserId());

            return endereco is null ? NotFound() : CustomResponse(endereco);
        }
    }
}