using NSE.Core.Messages;

namespace NSE.Pedido.API.Application.Events
{
    public sealed class PedidoRealizadoEvent:Event
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }

        public PedidoRealizadoEvent(Guid pedidoId, Guid clienteId)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }
    }
}