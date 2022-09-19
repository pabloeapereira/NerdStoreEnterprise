using NSE.Core.DomainObjects;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface ICarrinhoService
    {
        Task<CarrinhoViewModel> GetCarrinhoAsync();
        Task<ResponseResult> AdicionarItemCarrinhoAsync(ItemProdutoViewModel item);
        Task<ResponseResult> AtualizarItemCarrinhoAsync(Guid produtoId, ItemProdutoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinhoAsync(Guid produtoId);
    }
}
