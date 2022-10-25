using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.API.Application.Queries;
using NSE.WebAPI.Core.Controllers;
using System.Net;

namespace NSE.Pedido.API.Controllers
{
    [Authorize]
    [Route("vouchers")]
    public class VoucherController:MainController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(VoucherDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return NotFound();

            var voucher = await _voucherQueries.GetByCodigoAsync(codigo);
            return voucher is null ? NotFound() : CustomResponse(voucher);
        }
    }
}