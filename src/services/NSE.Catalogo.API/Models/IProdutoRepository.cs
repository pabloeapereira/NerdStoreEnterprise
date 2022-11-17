using NSE.Core.Data;

namespace NSE.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        ValueTask<Produto?>GetByIdAsync(Guid id);
        Task<List<Produto>> GetProductsById(string ids);
        void Add(Produto produto);
        void Update(Produto produto);
    }
}