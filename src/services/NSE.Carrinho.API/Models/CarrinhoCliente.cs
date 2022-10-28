using FluentValidation;
using FluentValidation.Results;

namespace NSE.Carrinho.API.Models
{
    public class CarrinhoCliente
    {
        internal const int MAX_QUANTIDADE_ITEM = 5;
        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }
        public CarrinhoCliente() { }
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
        public ValidationResult ValidationResult { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }
        public Voucher Voucher { get; private set; }

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorCarrinho();
        }

        private void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            var desconto = decimal.Zero;
            var valor = ValorTotal;

            if (Voucher.TipoDesconto == TipoDescontoVoucher.Porcentagem && Voucher.Percentual.HasValue)
            {
                desconto = (valor * Voucher.Percentual.Value);
                valor -= desconto;
            }
            else if (Voucher.ValorDesconto.HasValue)
            {
                desconto = Voucher.ValorDesconto.Value;
                valor -= desconto;
            }

            ValorTotal = valor < decimal.Zero ? decimal.Zero : valor;
            Desconto = desconto;
        }

        internal void CalcularValorCarrinho()
        {
            ValorTotal = Itens.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }

        internal bool CarrinhoItemExistente(CarrinhoItem item) => Itens.Any(p => p.ProdutoId == item.ProdutoId);

        internal CarrinhoItem ObterPorProdutoId(Guid produtoId) => Itens.FirstOrDefault(p => p.ProdutoId == produtoId);

        internal void AddItem(CarrinhoItem item)
        {
            item.SetCarrinho(Id);

            if (CarrinhoItemExistente(item))
            {
                var itemExistente = ObterPorProdutoId(item.ProdutoId);
                itemExistente.AddUnidades(item.Quantidade);

                item = itemExistente;
                Itens.Remove(itemExistente);
            }

            Itens.Add(item);
            CalcularValorCarrinho();
        }

        internal void AtualizarItem(CarrinhoItem item)
        {
            item.SetCarrinho(Id);
            var itemExistente = ObterPorProdutoId(item.ProdutoId);

            Itens.Remove(itemExistente);
            Itens.Add(item);
            CalcularValorCarrinho();
        }

        internal void AtualizarUnidades(CarrinhoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        internal void RemoverItem(CarrinhoItem item)
        {
            Itens.Remove(ObterPorProdutoId(item.ProdutoId));
            CalcularValorCarrinho();
        }

        internal bool IsValid()
        {
            var erros = Itens.SelectMany(i => new CarrinhoItem.ItemCarrinhoValidation().Validate(i).Errors).ToList();
            erros.AddRange(new CarrinhoClienteValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(erros);
            return ValidationResult.IsValid;
        }

        private class CarrinhoClienteValidation : AbstractValidator<CarrinhoCliente>
        {
            public CarrinhoClienteValidation()
            {
                RuleFor(c => c.ClienteId).NotEqual(Guid.Empty).WithMessage("Cliente não reconhecido");
                RuleFor(c => c.Itens.Count).GreaterThanOrEqualTo(0).WithMessage("O carrinho não possui itens");
                RuleFor(c => c.ValorTotal).GreaterThan(0).WithMessage("O valor total do carrinho precisa ser maior que 0.00");
            }
        }

    }
}