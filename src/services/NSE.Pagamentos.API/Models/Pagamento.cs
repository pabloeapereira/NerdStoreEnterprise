using NSE.Core.DomainObjects;

namespace NSE.Pagamentos.API.Models
{
    public sealed class Pagamento : Entity, IAggregateRoot
    {
        public Pagamento()
        {
        }

        public Guid PedidoId { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public decimal Valor { get; set; }

        public CartaoCredito CartaoCredito { get; set; }

        // EF Relation
        public ICollection<Transacao> Transacoes { get; set; } = Enumerable.Empty<Transacao>().ToList();

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
    }
}