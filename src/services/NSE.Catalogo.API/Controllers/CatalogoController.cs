using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo/produtos")]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>> Index() => await _produtoRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id) => await _produtoRepository.GetByIdAsync(id);

    }
}