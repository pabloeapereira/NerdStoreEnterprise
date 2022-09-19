using Microsoft.AspNetCore.Mvc;
using NSE.Core.Comunication;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta is not null && resposta.Errors?.Mensagens is not null)
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                    ModelState.TryAddModelError(string.Empty, mensagem);

                return true;
            }
            return false;
        }

        protected void AddError(string message) =>
            ModelState.TryAddModelError(string.Empty, message);

        protected bool OperacaoValida() => ModelState.ErrorCount <= 0;
    }
}