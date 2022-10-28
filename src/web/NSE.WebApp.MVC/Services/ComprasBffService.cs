using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface IComprasBffService
    {
        Task<CarrinhoViewModel> GetCarrinhoAsync();
        Task<int> GetQuantidadeCarrinhoAsync();
        Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemCarrinhoViewModel item);
        Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemCarrinhoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId);
        Task<ResponseResult> AplicarvoucherCarrinhoAsync(string voucher);
    }
    public class ComprasBffService : Service, IComprasBffService
    {
        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings) : base(httpClient, settings)
        {
            _httpClient.BaseAddress = new Uri($"{_appSettings.ComprasBffUrl}/compras/");
        }

        public async Task<CarrinhoViewModel> GetCarrinhoAsync()
        {
            var response = await _httpClient.GetAsync("carrinho");
            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<CarrinhoViewModel>(response);
        }

        public async Task<int> GetQuantidadeCarrinhoAsync()
        {
            var response = await _httpClient.GetAsync("carrinho-quantidade");
            TratarErrosResponse(response);

            return await DeserializeObjectResponseAsync<int>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemCarrinhoViewModel item)
        {
            var response = await _httpClient.PostAsJsonAsync("carrinho/itens", item, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemCarrinhoViewModel produto)
        {
            var response = await _httpClient.PutAsJsonAsync($"carrinho/itens/{produtoId}", produto, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"carrinho/itens/{produtoId}");
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }

        public async Task<ResponseResult> AplicarvoucherCarrinhoAsync(string voucher)
        {
            var response = await _httpClient.PostAsJsonAsync("carrinho/aplicar-voucher", voucher, JsonOptions);
            return await TratarErrosResponseERetornarResponseResultAsync(response);
        }
    }
}