using System.Net;
using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;
using NSE.Core.Comunication;

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

        public async Task<ResponseResult> FinalizarPedidoAsync(PedidoDTO pedido)
        {
            var response = await _httpClient.PostAsJsonAsync("/pedido/", pedido);

            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<PedidoDTO> ObterUltimoPedidoAsync()
        {
            var response = await _httpClient.GetAsync("/pedido/ultimo/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<PedidoDTO>(response);
        }

        public async Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId()
        {
            var response = await _httpClient.GetAsync("/pedido/lista-cliente/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<IEnumerable<PedidoDTO>>(response);
        }
    }

    public interface IPedidoService
    {
        Task<VoucherDTO> GetVoucherByCodigoAsync(string codigo);
        Task<ResponseResult> FinalizarPedidoAsync(PedidoDTO pedido);
        Task<PedidoDTO> ObterUltimoPedidoAsync();
        Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId();
    }
}