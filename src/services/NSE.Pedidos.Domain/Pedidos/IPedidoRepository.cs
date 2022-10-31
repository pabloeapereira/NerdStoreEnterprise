using NSE.Core.Data;

namespace NSE.Pedidos.Domain.Pedidos
{
    public interface IPedidoRepository : IRepository<Pedido>
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