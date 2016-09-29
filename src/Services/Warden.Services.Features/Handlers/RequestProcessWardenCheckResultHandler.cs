using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;
using RequestProcessWardenCheckResult = Warden.Common.Commands.Wardens.RequestProcessWardenCheckResult;

namespace Warden.Services.Features.Handlers
{
    public class RequestProcessWardenCheckResultHandler : ICommandHandler<Common.Commands.WardenChecks.RequestProcessWardenCheckResult>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestProcessWardenCheckResultHandler(IBusClient bus, IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(Common.Commands.WardenChecks.RequestProcessWardenCheckResult command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddWardenCheck);
            if (!featureAvailable)
                return;

            await _bus.PublishAsync(new RequestProcessWardenCheckResult(
                command.UserId, command.OrganizationId, command.WardenId, command.Check,
                command.CreatedAt));
        }
    }
}