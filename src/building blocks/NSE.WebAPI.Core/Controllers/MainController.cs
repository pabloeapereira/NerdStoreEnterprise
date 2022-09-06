using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens",Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            foreach (var erro in modelState.Values.SelectMany(e => e.Errors))
                AddError(erro.ErrorMessage);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            validationResult.Errors?.ToList().ForEach(error => AddError(error.ErrorMessage));

            return CustomResponse();
        }

        protected bool OperacaoValida() => !Errors.Any();

        protected void AddError(string erro) => Errors.Add(erro);

        protected void ClearErrors() => Errors.Clear();
    }
}