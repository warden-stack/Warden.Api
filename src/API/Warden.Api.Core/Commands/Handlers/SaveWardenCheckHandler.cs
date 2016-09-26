using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.WardenChecks;

namespace Warden.Api.Core.Commands.Handlers
{
    public class SaveWardenCheckHandler : ICommandHandler<SaveWardenCheck>
    {
        private readonly IUserFeaturesManager _userFeaturesManager;
        private readonly IWardenCheckService _wardenCheckService;

        public SaveWardenCheckHandler(IUserFeaturesManager userFeaturesManager, 
            IWardenCheckService wardenCheckService)
        {
            _userFeaturesManager = userFeaturesManager;
            _wardenCheckService = wardenCheckService;
        }

        public async Task HandleAsync(SaveWardenCheck command)
        {
            await _userFeaturesManager.UseFeatureIfAvailableAsync(command.AuthenticatedUserId,
                FeatureType.AddWardenCheck, async () => await _wardenCheckService.ProcessAsync(command.OrganizationId,
                    command.WardenId, command.Check));
        }
    }
}