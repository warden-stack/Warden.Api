using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Events.Features;
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
            {
                await _bus.PublishAsync(new FeatureRejected(command.Request.Id,
                    command.UserId, FeatureType.AddWardenCheck.ToString(),
                    "Warden check limit reached."));

                return;
            }

            await _bus.PublishAsync(new ProcessWardenCheckResult
            {
                UserId = command.UserId,
                CreatedAt = command.CreatedAt,
                OrganizationId = command.OrganizationId,
                WardenId = command.WardenId,
                Check = command.Check,
                Request = command.Request
            });
        }
    }
}