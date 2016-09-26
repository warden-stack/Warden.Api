using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Core.Commands.Handlers
{
    public class CreateWardenHandler : ICommandHandler<CreateWarden>
    {
        private readonly IWardenService _wardenService;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public CreateWardenHandler(IWardenService wardenService,
            IUserFeaturesManager userFeaturesManager)
        {
            _wardenService = wardenService;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(CreateWarden command)
        {
            await _userFeaturesManager.UseFeatureIfAvailableAsync(command.AuthenticatedUserId,
                FeatureType.AddWarden, async () => await _wardenService.CreateWardenAsync(command.WardenId,
                    command.OrganizationId, command.AuthenticatedUserId, command.Name));
        }
    }
}