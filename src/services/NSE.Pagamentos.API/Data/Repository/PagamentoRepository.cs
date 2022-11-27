using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pagamentos.API.Models;

namespace NSE.Pagamentos.API.Data.Repository
{
    public sealed class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentosContext _context;

        public PagamentoRepository(PagamentosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddPagamentoAsync(Pagamento pagamento) =>
            await _context.Pagamentos.AddAsync(pagamento);

        public async Task AddTransacaoAsync(Transacao transacao) =>
            await _context.Transacoes.AddAsync(transacao);

        public Task<Pagamento?> ObterPagamentoPorPedidoId(Guid pedidoId) =>
             _context.Pagamentos.AsNoTracking().FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        public Task<List<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId) =>
            _context.Transacoes.AsNoTracking().Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();

        public void Dispose() => _context.Dispose();

        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}