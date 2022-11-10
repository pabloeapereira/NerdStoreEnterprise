using NSE.Core.Data;
using NSE.Pedidos.Domain.Interfaces;

namespace NSE.Pedidos.Domain.Pedidos
{
    public interface IPedidoRepository : IRepository<Pedido>, IRepositoryDbConnection
    {
        ValueTask<Pedido?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pedido>> GetListByClienteIdAsync(Guid clienteId);
        Task AddAsync(Pedido entity);
        void Update(Pedido entity);

        /* Pedido Item*/
        ValueTask<PedidoItem?> GetItemByIdAsync(Guid id);
        Task<PedidoItem?> GetItemByPedidoAsync(Guid pedidoId, Guid produtoId);
    }
}