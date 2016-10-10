using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Wardens;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        private readonly IUserFeaturesManager _userFeaturesManager;

        public WardenCheckResultProcessedHandler(IUserFeaturesManager userFeaturesManager)
        {
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            await _userFeaturesManager.IncreaseFeatureUsageAsync(@event.UserId, FeatureType.AddWardenCheck);
        }
    }
}