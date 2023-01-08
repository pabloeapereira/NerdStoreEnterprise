using NSE.Core.Extensions;
using NSE.MessageBus;
using NSE.Pedido.API.Services;

namespace NSE.Pedido.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<PedidoOrquestradorIntegrationHandler>();
    }
}
