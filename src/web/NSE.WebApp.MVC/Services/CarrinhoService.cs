using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        public CarrinhoService(HttpClient httpClient, IOptions<AppSettings> settings) : base(httpClient, settings)
        {
            _httpClient.BaseAddress = new Uri(_appSettings.CarrinhoUrl);
        }

        public async Task<CarrinhoViewModel> GetCarrinhoAsync()
        {
            var response = await _httpClient.GetAsync("carrinhos");
            TratarErrosResponse(response);

            return await DeserializeObjectResponse<CarrinhoViewModel>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemProdutoViewModel item)
        {
            var response = await _httpClient.PostAsJsonAsync("carrinhos", item, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemProdutoViewModel produto)
        {
            var response = await _httpClient.PutAsJsonAsync($"carrinhos/{produtoId}", produto, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"carrinhos/{produtoId}");
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }
    }
}