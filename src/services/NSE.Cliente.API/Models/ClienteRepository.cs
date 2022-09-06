using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Data;
using NSE.Core.Data;

namespace NSE.Clientes.API.Models
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente) => await _context.Clientes.AddAsync(cliente);

        public async ValueTask<IEnumerable<Cliente>> GetAllAsync() => await _context.Clientes.AsNoTracking().ToListAsync();

        public Task<Cliente?> GetByCpfAsync(string cpf) => _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);

        public ValueTask<Cliente?> GetById(Guid id) => _context.Clientes.FindAsync(id);

        public Task<bool> ClienteExistsByCpfAsync(string cpf) => _context.Clientes.AnyAsync(c => c.Cpf.Numero == cpf);

        public void Dispose() => _context.Dispose();
    }
}