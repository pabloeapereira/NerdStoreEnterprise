using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;
using NSE.Core.Comunication;

namespace NSE.Bff.Compras.Services
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        public CarrinhoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemCarrinhoDTO item)
        {
            var response = await _httpClient.PostAsJsonAsync("carrinho", item, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemCarrinhoDTO produto)
        {
            var response = await _httpClient.PutAsJsonAsync($"carrinho/{produtoId}", produto, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<CarrinhoDTO> GetCarrinhoAsync()
        {
            var response = await _httpClient.GetAsync("carrinho");
            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<CarrinhoDTO>(response);
        }

        public async Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"carrinho/{produtoId}");
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }
    }

    public interface ICarrinhoService 
    {
        Task<CarrinhoDTO> GetCarrinhoAsync();
        Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemCarrinhoDTO item);
        Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemCarrinhoDTO produto);
        Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId);
    }
}