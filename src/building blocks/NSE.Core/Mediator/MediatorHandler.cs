using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;

namespace NSE.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEventAsync<T>(T eventData) where T : Event =>
            _mediator.Publish(eventData);

        public Task<ValidationResult> SendCommandAsync<T>(T command) where T : Command =>
            _mediator.Send(command);
    }
}