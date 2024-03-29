﻿using MediatR;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Pedido.API.Application.Events
{
    public sealed class PedidoEventHandler:INotificationHandler<PedidoRealizadoEvent>
    {
        private readonly IMessageBus _bus;

        public PedidoEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(PedidoRealizadoEvent message, CancellationToken cancellationToken) =>
            _bus.PublishAsync(new PedidoRealizadoIntegrationEvent(message.ClienteId), cancellationToken);
    }
}