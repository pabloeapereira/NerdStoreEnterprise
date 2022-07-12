using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel { Code = id };
            switch (id)
            {
                case 500:
                    modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelError.Title = "Ocorreu um erro!";
                    break;
                case 404:
                    modelError.Message = "A ágina que está procurando não existe! <br/>Em caso de dúvidas entre em contato com o nosso suporte.";
                    modelError.Title = "Ops! Página não encontrada";
                    break;
                case 403:
                    modelError.Message = "Você não tem permissão para fazer isto.";
                    modelError.Title = "Acesso Negado";
                    break;
                default:
                    return NotFound();
            };

            return View(modelError);
        }
    }
}