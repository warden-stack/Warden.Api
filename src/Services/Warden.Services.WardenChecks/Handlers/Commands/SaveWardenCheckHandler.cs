using System.Threading.Tasks;
using Warden.Common.Commands;

namespace Warden.Services.WardenChecks.Handlers.Commands
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