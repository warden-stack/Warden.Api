using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.WardenChecks;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestWardenCheckResultProcessingResultHandler : ICommandHandler<RequestWardenCheckResultProcessing>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestWardenCheckResultProcessingResultHandler(IBusClient bus, IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(RequestWardenCheckResultProcessing command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddWardenCheck);
            if (!featureAvailable)
                return;

            await _bus.PublishAsync(new ProcessWardenCheckResult
            {
                UserId = command.UserId,
                CreatedAt = command.CreatedAt,
                OrganizationId = command.OrganizationId,
                WardenId = command.WardenId,
                Check = command.Check,
                Details = command.Details.Copy()
            });
        }
    }
}