using NSE.Core.Messages.Integration;

namespace NSE.Pedido.API.Application.Events
{
    public sealed class PedidoAutorizadoIntegrationEvent : IntegrationEvent
    {
        public PedidoAutorizadoIntegrationEvent(Guid clientId, Guid pedidoId, IDictionary<Guid, int> itens)
        {
            ClientId = clientId;
            PedidoId = pedidoId;
            Itens = itens;
        }

        public Guid ClientId { get; private set; }
        public Guid PedidoId { get; private set; }
        public IDictionary<Guid,int> Itens { get; private set; }
    }
}