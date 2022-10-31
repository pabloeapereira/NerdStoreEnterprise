using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedidos.Infra.Data.Repository
{
    public class PedidoRepository: IPedidoRepository
    {
        private readonly PedidosContext _context;
        public PedidoRepository(PedidosContext context)
        {
            _context = context;
        }
        
        public IUnitOfWork UnitOfWork  => _context;

        public ValueTask<Pedido?> GetByIdAsync(Guid id) => _context.Pedidos.FindAsync(id);

        public async Task<IEnumerable<Pedido>> GetListByClienteIdAsync(Guid clienteId) =>
            await _context.Pedidos.Include(p => p.PedidoItens)
                .AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();

        public async Task AddAsync(Pedido entity) => await _context.Pedidos.AddAsync(entity);

        public void Update(Pedido entity) =>  _context.Pedidos.Update(entity);

        public ValueTask<PedidoItem?> GetItemByIdAsync(Guid id) => _context.PedidoItens.FindAsync(id);

        public Task<PedidoItem?> GetItemByPedidoAsync(Guid pedidoId, Guid produtoId) =>
            _context.PedidoItens.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);

        public void Dispose() => _context.Dispose();
    }
}