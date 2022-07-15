using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;

namespace NSE.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;
        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public void Add(Produto produto) => _context.Produtos.Add(produto);

        public async Task<IEnumerable<Produto>> GetAllAsync() => await _context.Produtos.AsNoTracking().ToListAsync();

        public ValueTask<Produto?> GetByIdAsync(Guid id) => _context.Produtos.FindAsync(id);

        public void Update(Produto produto) => _context.Produtos.Update(produto);

        public IUnitOfWork UnitOfWork => _context;
        public void Dispose() => _context?.Dispose();
    }
}