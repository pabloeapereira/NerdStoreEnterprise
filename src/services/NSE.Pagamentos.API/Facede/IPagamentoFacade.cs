using NSE.Pagamentos.API.Models;

namespace NSE.Pagamentos.API.Facede
{
    public interface IPagamentoFacade
    {
        ValueTask<Transacao> AutorizarPagamentoAsync(Pagamento pagamento);
        ValueTask<Transacao> CapturarPagamentoAsync(Transacao transacao);
        ValueTask<Transacao> CancelarAutorizacaoAsync(Transacao transacao);
    }
}