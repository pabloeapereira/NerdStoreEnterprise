using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;

namespace NSE.Bff.Compras.Services
{
    public class PagamentoService : Service, IPagamentoService
    {
        public PagamentoService(HttpClient httpClient, IOptions<AppServicesSettings> settings) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(settings.Value.PagamentoUrl);
        }
    }

    public interface IPagamentoService { }
}
