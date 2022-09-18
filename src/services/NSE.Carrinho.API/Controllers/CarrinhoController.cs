using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.API.Data;
using NSE.Carrinho.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    [Route("carrinhos")]
    public sealed class CarrinhoController:MainController
    {
        private readonly IAspNetUser _user;
        private readonly CarrinhoContext _context;

        public CarrinhoController(IAspNetUser user, CarrinhoContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet]
        public async Task<CarrinhoCliente> GetCarrinho() =>
            await ObterCarrinhoClienteAsync() ?? new ();

        [HttpPost]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoClienteAsync();
            if (carrinho is null)
                ManipularNovoCarrinho(item);
            else
                ManipularCarrinhoExistente(carrinho, item);

            ValidarCarrinho(carrinho);
            if (!OperacaoValida()) return CustomResponse();

            await SaveChangesAsync();
            return CustomResponse();
        }

        

        [HttpPut("{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoClienteAsync();
            var itemCarrinho = await ObterItemCarrinhoValidadoAsync(produtoId, carrinho, item);
            if (itemCarrinho is null) return CustomResponse();

            carrinho.AtualizarUnidades(itemCarrinho, item.Quantidade);

            ValidarCarrinho(carrinho);
            if (!OperacaoValida()) return CustomResponse();

            _context.CarrinhoItens.Update(itemCarrinho);
            _context.CarrinhoClientes.Update(carrinho);

            await SaveChangesAsync();
            return CustomResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var carrinho = await ObterCarrinhoClienteAsync();

            var itemCarrinho = await ObterItemCarrinhoValidadoAsync(produtoId, carrinho);
            if (itemCarrinho is null) return CustomResponse();

            ValidarCarrinho(carrinho);
            if (!OperacaoValida()) return CustomResponse();

            carrinho.RemoverItem(itemCarrinho);

            _context.CarrinhoItens.Remove(itemCarrinho);
            _context.CarrinhoClientes.Update(carrinho);

            await SaveChangesAsync();
            return CustomResponse();
        }

        #region Private Methods

        private Task<CarrinhoCliente?> ObterCarrinhoClienteAsync() =>
            _context.CarrinhoClientes.Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUserId());

        private void ManipularNovoCarrinho(CarrinhoItem item)
        {
            var carrinho = new CarrinhoCliente(_user.ObterUserId());
            carrinho.AddItem(item);
            _context.CarrinhoClientes.Add(carrinho);
        }

        private void ManipularCarrinhoExistente(CarrinhoCliente carrinho, CarrinhoItem item)
        {
            var produtoItemExistente = carrinho.CarrinhoItemExistente(item);
            carrinho.AddItem(item);

            if (produtoItemExistente)
                _context.CarrinhoItens.Update(carrinho.ObterPorProdutoId(item.ProdutoId));
            else
                _context.CarrinhoItens.Add(item);

            _context.CarrinhoClientes.Update(carrinho);
        }

        private async Task<CarrinhoItem> ObterItemCarrinhoValidadoAsync(Guid produtoId, CarrinhoCliente carrinho, CarrinhoItem? item = null)
        {
            if(item is not null && produtoId != item.ProdutoId)
            {
                AddError("O item não corresponde ao informado");
                return null;
            }

            if(carrinho is null)
            {
                AddError("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await _context.CarrinhoItens.FirstOrDefaultAsync(i => i.CarrinhoId == carrinho.Id && i.ProdutoId == produtoId);

            if(itemCarrinho is null || !carrinho.CarrinhoItemExistente(itemCarrinho)){
                AddError("O item não está no carrinho");
                return null;
            }

            return itemCarrinho;
        }

        private async Task SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddError("Não foi possível persistir os dados no banco");
        }

        private bool ValidarCarrinho(CarrinhoCliente carrinho)
        {
            if (carrinho.IsValid()) return true;
            AddError(carrinho.ValidationResult);
            return false;
        }
        #endregion
    }
}