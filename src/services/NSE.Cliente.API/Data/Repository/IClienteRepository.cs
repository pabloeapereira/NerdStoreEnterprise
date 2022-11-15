using NSE.Clientes.API.Models;
using NSE.Core.Data;

namespace NSE.Clientes.API.Data.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task AddAsync(Cliente cliente);
        ValueTask<IEnumerable<Cliente>> GetAllAsync();
        ValueTask<Cliente?> GetById(Guid id);
        Task<Cliente?> GetByCpfAsync(string cpf);
        Task<bool> ClienteExistsByCpfAsync(string cpf);
        Task<Endereco?> GetEnderecoByClientIdAsync(Guid id);
        Task AddAsync(Endereco endereco);
    }
}