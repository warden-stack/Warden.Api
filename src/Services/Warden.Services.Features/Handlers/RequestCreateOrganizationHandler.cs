using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestCreateOrganizationHandler : ICommandHandler<RequestCreateOrganization>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestCreateOrganizationHandler(IBusClient bus, IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(RequestCreateOrganization command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddOrganization);
            if (!featureAvailable)
                return;

            await _bus.PublishAsync(new CreateOrganization
            {
                UserId = command.UserId,
                Name = command.Name,
                Description = command.Description
            });
        }
    }
}