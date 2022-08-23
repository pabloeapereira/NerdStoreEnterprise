using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        ValueTask<IEnumerable<ProdutoViewModel>> GetAllAsync();
        ValueTask<ProdutoViewModel> GetByIdAsync(Guid id);
    }
}
