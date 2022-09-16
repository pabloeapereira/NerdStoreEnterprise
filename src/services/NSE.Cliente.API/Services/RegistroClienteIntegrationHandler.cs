using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarClienteAsync(request), stoppingToken);

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> RegistrarClienteAsync(UsuarioRegistradoIntegrationEvent usuario)
        {
            var clienteCommand = new RegistrarClienteCommand(usuario.Id, usuario.Nome, usuario.Email, usuario.Cpf);

            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            return new ResponseMessage(await mediator.SendCommandAsync(clienteCommand));

        }
    }
}