using NSE.Core.Data;

namespace NSE.Clientes.API.Models
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task AddAsync(Cliente cliente);
        ValueTask<IEnumerable<Cliente>> GetAllAsync();
        ValueTask<Cliente?> GetById(Guid id);
        Task<Cliente?> GetByCpfAsync(string cpf);
        Task<bool> ClienteExistsByCpfAsync(string cpf);
    }
}