using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Common.Events.Features;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestNewOrganizationHandler : ICommandHandler<RequestNewOrganization>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestNewOrganizationHandler(IBusClient bus, IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(RequestNewOrganization command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddOrganization);
            if (!featureAvailable)
            {
                await _bus.PublishAsync(new FeatureRejected(command.Request.Id,
                    command.UserId, FeatureType.AddOrganization.ToString(),
                    "Organization limit reached."));

                return;
            }

            await _bus.PublishAsync(new CreateOrganization
            {
                OrganizationId = command.OrganizationId,
                UserId = command.UserId,
                Name = command.Name,
                Description = command.Description,
                Request = command.Request
            });
        }
    }
}