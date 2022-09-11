using EasyNetQ;
using FluentValidation.Results;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");
            _bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
            new ResponseMessage(await RegistrarClienteAsync(request)), cancellationToken: stoppingToken);

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegistrarClienteAsync(UsuarioRegistradoIntegrationEvent usuario)
        {
            var clienteCommand = new RegistrarClienteCommand(usuario.Id, usuario.Nome, usuario.Email, usuario.Cpf);

            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            return await mediator.SendCommandAsync(clienteCommand);

        }
    }
}
