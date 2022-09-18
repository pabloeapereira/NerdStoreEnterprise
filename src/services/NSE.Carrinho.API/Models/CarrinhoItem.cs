using FluentValidation;
using NSE.Core.Validations;

namespace NSE.Carrinho.API.Models
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        public Guid CarrinhoId { get; set; }
        public CarrinhoCliente CarrinhoCliente { get; set; }

        internal void SetCarrinho(Guid carrinhoId) =>
            CarrinhoId = carrinhoId;

        internal decimal CalcularValor() => Quantidade * Valor;

        internal void AddUnidades(int unidades) => Quantidade += unidades;

        internal void AtualizarUnidades(int unidades) => Quantidade = unidades;

        internal bool IsValid() => new ItemCarrinhoValidation().Validate(this).IsValid;

        public class ItemCarrinhoValidation: AbstractValidator<CarrinhoItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(x => x.ProdutoId).NotEqual(Guid.Empty).WithMessage("Id do produto inválido");
                RuleFor(x => x.Nome).NotEmpty().WithMessage(ValidationMessages.Required);
                RuleFor(x => x.Quantidade).GreaterThan(0).WithMessage(item => $"A quantidade mínima para o {item.Nome} é 1")
                    .LessThan(CarrinhoCliente.MAX_QUANTIDADE_ITEM).WithMessage("A quantidade máxima de um item é {LessThan}");
                RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valor do item precisa ser maior que 0.00");
            }
        }
    }
}