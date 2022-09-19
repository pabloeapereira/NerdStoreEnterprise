using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;

namespace NSE.Bff.Compras.Services
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        public CarrinhoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }
    }

    public interface ICarrinhoService { }
}