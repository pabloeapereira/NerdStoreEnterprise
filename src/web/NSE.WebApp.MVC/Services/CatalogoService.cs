using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings) : base(httpClient, settings)
        {
            _httpClient.BaseAddress = new Uri(_appSettings.CatalogoUrl);
        }

        public async ValueTask<IEnumerable<ProdutoViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("/catalogo/produtos/");
            TratarErrosResponse(response);
            return await DeserializeObjectResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async ValueTask<ProdutoViewModel> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");
            TratarErrosResponse(response);
            return await DeserializeObjectResponse<ProdutoViewModel>(response);
        }
    }
}