using EasyNetQ;
using EasyNetQ.Internals;
using NSE.Core.Messages.Integration;
using System.Threading;

namespace NSE.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }
        void Publish<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent;
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent;
        void Subscribe<T>(string subscriptionId, Action<T> onMessage, CancellationToken cancellationToken = default) where T : class;
        Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage, CancellationToken cancellationToken = default) where T : class;
        TResponse Request<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IntegrationEvent where TResponse : ResponseMessage;
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IntegrationEvent where TResponse : ResponseMessage;
        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder, CancellationToken cancellationToken = default) where TRequest : IntegrationEvent where TResponse : ResponseMessage;
        AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder, CancellationToken cancellationToken = default) 
            where TRequest : IntegrationEvent where TResponse : ResponseMessage;
    }
}