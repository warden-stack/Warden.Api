using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.Features;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestNewWardenHandler : ICommandHandler<RequestNewWarden>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestNewWardenHandler(IBusClient bus, 
            IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(RequestNewWarden command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddWarden);
            if (!featureAvailable)
            {
                await _bus.PublishAsync(new FeatureRejected(command.Request.Id,
                    command.UserId, FeatureType.AddWarden.ToString(),
                    "Warden limit reached."));

                return;
            }

            await _bus.PublishAsync(new CreateWarden
            {
                UserId = command.UserId,
                Name = command.Name,
                WardenId = command.WardenId,
                OrganizationId = command.OrganizationId,
                Enabled = command.Enabled,
                Request = command.Request
            });
        }
    }
}