using NSE.Core.DomainObjects;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pagamentos.API.Models;

namespace NSE.Pagamentos.API.Services
{
    public class PagamentoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PagamentoIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder(CancellationToken cancellationToken)
        {
            _bus.RespondAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(async request =>
                await AutorizarPagamentoAsync(request), cancellationToken);
        }

        private void SetSubscribers(CancellationToken cancellationToken)
        {
            /*_bus.SubscribeAsync<PedidoCanceladoIntegrationEvent>("PedidoCancelado", async request =>
            await CancelarPagamentoAsync(request), cancellationToken);

            _bus.SubscribeAsync<PedidoBaixadoEstoqueIntegrationEvent>("PedidoBaixadoEstoque", async request =>
            await CapturarPagamento(request), cancellationToken);*/
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder(stoppingToken);
            SetSubscribers(stoppingToken);
            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> AutorizarPagamentoAsync(PedidoIniciadoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
            var pagamento = new Pagamento
            {
                PedidoId = message.PedidoId,
                TipoPagamento = (TipoPagamento)message.TipoPagamento,
                Valor = message.Valor,
                CartaoCredito = new CartaoCredito(
                    message.NomeCartao, message.NumeroCartao, message.MesAnoVencimento, message.CVV)
            };

            var response = await pagamentoService.AutorizarPagamentoAsync(pagamento);

            return response;
        }

        /*private async Task CancelarPagamentoAsync(PedidoCanceladoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();

                var response = await pagamentoService.CancelarPagamentoAsync(message.PedidoId);

                if (!response.ValidationResult.IsValid)
                    throw new DomainException($"Falha ao cancelar pagamento do pedido {message.PedidoId}");
            }
        }

        private async Task CapturarPagamento(PedidoBaixadoEstoqueIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();

                var response = await pagamentoService.CapturarPagamento(message.PedidoId);

                if (!response.ValidationResult.IsValid)
                    throw new DomainException($"Falha ao capturar pagamento do pedido {message.PedidoId}");

                await _bus.PublishAsync(new PedidoPagoIntegrationEvent(message.ClienteId, message.PedidoId));
            }
        }*/
    }
}