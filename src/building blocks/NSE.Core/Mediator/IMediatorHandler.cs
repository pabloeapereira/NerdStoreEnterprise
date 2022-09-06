using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T eventData) where T : Event;
        Task<ValidationResult> SendCommandAsync<T>(T command) where T : Command;
    }
}