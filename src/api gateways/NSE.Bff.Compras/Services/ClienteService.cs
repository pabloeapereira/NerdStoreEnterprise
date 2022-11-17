using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using System.Net;
using NSE.Bff.Compras.Models;

namespace NSE.Bff.Compras.Services
{
    public interface IClienteService
    {
        Task<EnderecoDTO> ObterEndereco();
    }

    public class ClienteService : Service, IClienteService
    {

        public ClienteService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }

        public async Task<EnderecoDTO> ObterEndereco()
        {
            var response = await _httpClient.GetAsync("/clientes/endereco/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<EnderecoDTO>(response);
        }
    }
}