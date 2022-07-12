using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin?> LoginAsync(UsuarioLogin usuarioLogin);
        Task<UsuarioRespostaLogin?> RegistroAsync(UsuarioRegistro usuarioRegistro);
    }
}