using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;

namespace NSE.Bff.Compras.Services
{
    public class PedidoService : Service, IPedidoService
    {
        public PedidoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
        }
    }
    public interface IPedidoService { }
}