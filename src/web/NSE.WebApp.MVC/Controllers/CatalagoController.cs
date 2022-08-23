using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalagoController : MainController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalagoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index() => View(await _catalogoService.GetAllAsync());

        [HttpGet("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id) => View(await _catalogoService.GetByIdAsync(id));
    }
}