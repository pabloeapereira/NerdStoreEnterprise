using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo/produtos")]
    public class CatalogoController : MainController
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

        [HttpGet("lista/{ids}")]
        public async Task<IEnumerable<Produto>> ObterProdutosPorId(string ids)
        {
            return await _produtoRepository.GetProductsById(ids);
        }

    }
}