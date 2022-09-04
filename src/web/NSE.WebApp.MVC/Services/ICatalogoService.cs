using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        ValueTask<IEnumerable<ProdutoViewModel>> GetAllAsync();
        ValueTask<ProdutoViewModel> GetByIdAsync(Guid id);
    }

    public interface ICatalagoServiceRefit
    {
        [Get("/catalogo/produtos/")]
        Task<IEnumerable<ProdutoViewModel>> GetAllAsync();

        [Get("/catalogo/produtos/{id}")]
        Task<ProdutoViewModel> GetByIdAsync(Guid id);
    }
}