using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;

namespace NSE.Bff.Compras.Services
{
    public class CatalogoService : Service,ICatalogoService
    {
        public CatalogoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<ItemProdutoDTO> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"catalogo/produtos/{id}");
            return TratarErrosResponse(response) ? await DeserializeObjectResponseAsync<ItemProdutoDTO>(response) : default;
        }
    }

    public interface ICatalogoService 
    {
        Task<ItemProdutoDTO> GetByIdAsync(Guid id);
    }
}