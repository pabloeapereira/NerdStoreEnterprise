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

        public async Task<List<Produto>> GetProductsById(string ids)
        {
            var idsGuid = ids.Split(',')
                .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            var valueTuples = idsGuid as (bool Ok, Guid Value)[] ?? idsGuid.ToArray();
            if (!valueTuples.All(nid => nid.Ok)) return new List<Produto>();

            var idsValue = valueTuples.Select(id => id.Value);

            return await _context.Produtos.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Ativo).ToListAsync();
        }

        public void Update(Produto produto) => _context.Produtos.Update(produto);

        public IUnitOfWork UnitOfWork => _context;
        public void Dispose() => _context?.Dispose();
    }
}