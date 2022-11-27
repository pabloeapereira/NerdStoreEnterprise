using NSE.Core.Data;
using NSE.Pagamentos.API.Models;

namespace NSE.Pagamentos.API.Data.Repository
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task AddPagamentoAsync(Pagamento pagamento);
        Task AddTransacaoAsync(Transacao transacao);
        Task<Pagamento?> ObterPagamentoPorPedidoId(Guid pedidoId);
        Task<List<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId);
    }
}