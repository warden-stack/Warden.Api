using System;
using System.Threading.Tasks;

namespace Warden.Common.Commands.Wardens
{
    public class CreateWarden : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }

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