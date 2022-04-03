using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    public abstract class MainController:Controller
    {
        protected ICollection<string> Erros = new List<string>();
        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens",Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            foreach(var erro in modelState.Values.SelectMany(e => e.Errors))
                AddErro(erro.ErrorMessage);

            return CustomResponse();
        }

        protected bool OperacaoValida() => !Erros.Any();

        protected void AddErro(string erro) => Erros.Add(erro);

        protected void LimparErros() => Erros.Clear();
    }
}
