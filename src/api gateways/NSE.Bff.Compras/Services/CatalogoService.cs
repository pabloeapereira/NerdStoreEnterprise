using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;

namespace NSE.Bff.Compras.Services
{
    public class CatalogoService : Service,ICatalogoService
    {
        public CatalogoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }
    }

    public interface ICatalogoService { }
}