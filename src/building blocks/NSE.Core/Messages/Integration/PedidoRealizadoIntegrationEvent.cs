namespace NSE.Core.Messages.Integration
{
    public sealed class PedidoRealizadoIntegrationEvent : IntegrationEvent
    {
        public Guid ClienteId { get; init; }

        public PedidoRealizadoIntegrationEvent(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}