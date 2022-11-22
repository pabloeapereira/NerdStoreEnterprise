using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Clientes.API.Data.Repository;
using NSE.Clientes.API.Models;
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

        [HttpPost("endereco")]
        public async Task<IActionResult> AdicionarEndereco(EnderecoViewModel endereco)
        {
            var enderecoCommand = new AdicionarEnderecoCommand(_user.ObterUserId(), endereco.Logradouro, endereco.Numero, endereco.Complemento,
                endereco.Bairro, endereco.Cep, endereco.Cidade, endereco.Estado);
            return CustomResponse(await _mediatorHandler.SendCommandAsync(enderecoCommand));

            
        }

        public class EnderecoViewModel
        {
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string? Complemento { get; set; }
            public string Bairro { get; set; }
            public string Cep { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
        }
    }
}