using System.Net;
using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public sealed class ClienteService : Service, IClienteService
    {
        public ClienteService(HttpClient httpClient, IOptions<AppSettings> settings) : base(httpClient, settings)
        {
            _httpClient.BaseAddress = new Uri($"{_appSettings.ClienteUrl}/clientes/");
        }

        public async ValueTask<EnderecoViewModel?> GetEnderecoAsync()
        {
            var response = await _httpClient.GetAsync("endereco");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<EnderecoViewModel>(response);
        }

        public async ValueTask<ResponseResult> AddEnderecoAsync(EnderecoViewModel endereco) =>
            await TratarErrosResponseERetornarResponseResultAsync(await _httpClient.PostAsJsonAsync("endereco", endereco, JsonOptions));
    }
}
