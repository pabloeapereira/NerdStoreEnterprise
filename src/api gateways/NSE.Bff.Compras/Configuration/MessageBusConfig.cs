using NSE.Core.Extensions;
using NSE.MessageBus;

namespace NSE.Bff.Compras.Configuration
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services;
    }
}