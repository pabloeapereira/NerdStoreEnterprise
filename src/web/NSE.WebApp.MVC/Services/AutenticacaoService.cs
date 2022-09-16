using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> settings):base(httpClient, settings)
        {
            _httpClient.BaseAddress = new Uri(_appSettings.AutenticacaoUrl);
        }

        public async Task<UsuarioRespostaLogin?> LoginAsync(UsuarioLogin usuarioLogin)
        {
            var loginContent = GetContent(usuarioLogin);
            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin { ResponseResult = await DeserializeObjectResponse<ResponseResult>(response) };
            };

            return await DeserializeObjectResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin?> RegistroAsync(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = GetContent(usuarioRegistro);
            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {

                return new UsuarioRespostaLogin { ResponseResult = await DeserializeObjectResponse<ResponseResult>(response) };
            };

            return (await DeserializeObjectResponse<GenericResponseViewModel<UsuarioRespostaLogin>>(response)).Value;
        }
    }
}