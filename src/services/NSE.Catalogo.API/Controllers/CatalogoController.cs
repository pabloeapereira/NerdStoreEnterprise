using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo/produtos")]
    [Authorize]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<Produto>> Index() => await _produtoRepository.GetAllAsync();

        [HttpGet("{id}"), ClaimsAuthorize("Catalogo", "Ler")]
        public async Task<Produto> ProdutoDetalhe(Guid id) => await _produtoRepository.GetByIdAsync(id);

    }
}