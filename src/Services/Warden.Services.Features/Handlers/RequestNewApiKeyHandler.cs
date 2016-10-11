using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestNewApiKeyHandler : ICommandHandler<RequestNewApiKey>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestNewApiKeyHandler(IBusClient bus, IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        //TODO: Implement rejected operation.
        public async Task HandleAsync(RequestNewApiKey command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddApiKey);
            if (!featureAvailable)
                return;

            await _bus.PublishAsync(new CreateApiKey
            {
                ApiKeyId = command.ApiKeyId,
                UserId = command.UserId,
                Details = command.Details.Copy()
            });
        }
    }
}