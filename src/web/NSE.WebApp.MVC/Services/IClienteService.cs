using NSE.Core.Comunication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface IClienteService
    {
        ValueTask<EnderecoViewModel?> GetEnderecoAsync();
        ValueTask<ResponseResult> AddEnderecoAsync(EnderecoViewModel endereco);
    }
}