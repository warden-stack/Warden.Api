using System;
using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Events;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Features;
using Warden.Common.Events.Operations;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Wardens;
using Warden.Services.Operations.Domain;
using Warden.Services.Operations.Services;

namespace Warden.Services.Operations.Handlers
{
    public class GenericEventHandler : IEventHandler<ApiKeyCreated>, 
        IEventHandler<OrganizationCreated>,
        IEventHandler<WardenCreated>, IEventHandler<WardenCheckResultProcessed>,
        IEventHandler<FeatureRejected>
    {
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericEventHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(ApiKeyCreated @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(WardenCreated @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(OrganizationCreated @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(WardenCheckResultProcessed @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(FeatureRejected @event)
            => await RejectAsync(@event);

        private async Task CompleteAsync(IAuthenticatedEvent @event)
        {
            await _operationService.CompleteAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                @event.UserId, States.Completed, DateTime.UtcNow, string.Empty));
        }

        private async Task RejectAsync(IRejectedEvent @event)
        {
            await _operationService.RejectAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                @event.UserId, States.Rejected, DateTime.UtcNow, @event.Reason));
        }
    }
}