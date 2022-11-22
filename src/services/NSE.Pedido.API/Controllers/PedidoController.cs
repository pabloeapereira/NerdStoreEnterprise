using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NSE.Core.Mediator;
using NSE.Pedido.API.Application.Commands;
using NSE.Pedido.API.Application.Queries;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Pedido.API.Controllers
{
    [Authorize,Route("pedido")]
    public sealed class PedidoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IMediatorHandler mediator, IAspNetUser user, IPedidoQueries pedidoQueries)
        {
            _mediator = mediator;
            _user = user;
            _pedidoQueries = pedidoQueries;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPedido(AdicionarPedidoCommand pedido)
        {
            pedido.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediator.SendCommandAsync(pedido));
        }

        [HttpGet("ultimo")]
        public async Task<IActionResult> UltimoPedido()
        {
            var pedido = await _pedidoQueries.GetUltimoPedidoAsync(_user.ObterUserId());
            return pedido is null ? NotFound() : CustomResponse(pedido);
        }

        [HttpGet("lista-cliente")]
        public async Task<IActionResult> ListarPorCliente()
        {
            var pedidos = await _pedidoQueries.GetListByClienteIdAsync(_user.ObterUserId());
            return pedidos is null ? NotFound() : CustomResponse(pedidos);
        }
    }
}