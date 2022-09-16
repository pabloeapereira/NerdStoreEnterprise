using EasyNetQ;
using EasyNetQ.Internals;
using NSE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace NSE.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public void Publish<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message,cancellationToken);
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent
        {
            TryConnect();
            return _bus.PubSub.PublishAsync(message,cancellationToken);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request,cancellationToken);
        }

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.RequestAsync<TRequest, TResponse>(request, cancellationToken);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder, CancellationToken cancellationToken = default)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Respond(responder,cancellationToken);
        }
        public AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder, CancellationToken cancellationToken = default)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.RespondAsync(responder, cancellationToken);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage, CancellationToken cancellationToken = default) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscriptionId, onMessage,cancellationToken);
        }

        public Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage, CancellationToken cancellationToken = default) where T : class
        {
            TryConnect();
            return _bus.PubSub.SubscribeAsync(subscriptionId, onMessage,cancellationToken);
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => { _bus = RabbitHutch.CreateBus(_connectionString); });
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}