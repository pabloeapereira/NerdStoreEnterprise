using System.Net;
using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;

namespace NSE.Bff.Compras.Services
{
    public class PedidoService : Service, IPedidoService
    {
        public PedidoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
        }

        public async Task<VoucherDTO> GetVoucherByCodigoAsync(string codigo)
        {
            var response = await _httpClient.GetAsync($"vouchers/{codigo}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            TratarErrosResponse(response);
            return await DeserializeObjectResponseAsync<VoucherDTO>(response);
        }
    }

    public interface IPedidoService
    {
        Task<VoucherDTO> GetVoucherByCodigoAsync(string codigo);
    }
}