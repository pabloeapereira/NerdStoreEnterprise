namespace NSE.Core.Messages.Integration
{
    public class PedidoIniciadoIntegrationEvent : IntegrationEvent
    {
        public PedidoIniciadoIntegrationEvent(Guid clienteId, Guid pedidoId, decimal valor, string nomeCartao,
            string numeroCartao, string mesAnoVencimento, string cVV, int tipoPagamento = 1)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            TipoPagamento = tipoPagamento;
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            MesAnoVencimento = mesAnoVencimento;
            CVV = cVV;
        }

        public Guid ClienteId { get; set; }
        public Guid PedidoId { get; set; }
        public int TipoPagamento { get; set; }
        public decimal Valor { get; set; }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string MesAnoVencimento { get; set; }
        public string CVV { get; set; }
    }
}