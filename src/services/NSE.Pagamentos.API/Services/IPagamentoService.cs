using NSE.Core.Messages.Integration;
using NSE.Pagamentos.API.Models;

namespace NSE.Pagamentos.API.Services
{
    public interface IPagamentoService
    {
        Task<ResponseMessage> AutorizarPagamentoAsync(Pagamento pagamento);
        Task<ResponseMessage> CapturarPagamentoAsync(Guid pedidoId);
        Task<ResponseMessage> CancelarPagamentoAsync(Guid pedidoId);
    }
}