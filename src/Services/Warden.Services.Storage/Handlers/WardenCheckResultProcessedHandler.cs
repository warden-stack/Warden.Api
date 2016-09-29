using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Wardens;
using Warden.Services.Storage.Services;

namespace Warden.Services.Storage.Handlers
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        private readonly IWardenCheckResultRootService _wardenCheckResultRootService;

        public WardenCheckResultProcessedHandler(IWardenCheckResultRootService wardenCheckResultRootService)
        {
            _wardenCheckResultRootService = wardenCheckResultRootService;
        }

        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            await _wardenCheckResultRootService.ValidateAndAddAsync(@event.UserId,
                @event.OrganizationId, @event.WardenId, @event.Result, @event.CreatedAt);
        }
    }
}