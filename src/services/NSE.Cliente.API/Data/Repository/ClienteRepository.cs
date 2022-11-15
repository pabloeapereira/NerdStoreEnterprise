using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Data;
using NSE.Clientes.API.Models;
using NSE.Core.Data;

namespace NSE.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente) => await _context.Clientes.AddAsync(cliente);

        public async ValueTask<IEnumerable<Cliente>> GetAllAsync() => await _context.Clientes.AsNoTracking().ToListAsync();

        public Task<Cliente?> GetByCpfAsync(string cpf) => _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);

        public ValueTask<Cliente?> GetById(Guid id) => _context.Clientes.FindAsync(id);

        public Task<bool> ClienteExistsByCpfAsync(string cpf) => _context.Clientes.AnyAsync(c => c.Cpf.Numero == cpf);

        public Task<Endereco?> GetEnderecoByClientIdAsync(Guid id) => _context.Enderecos.FirstOrDefaultAsync(x => x.ClienteId == id);

        public async Task AddAsync(Endereco endereco) => await _context.Enderecos.AddAsync(endereco);

        public IUnitOfWork UnitOfWork => _context;
        public void Dispose() => _context.Dispose();
    }
}