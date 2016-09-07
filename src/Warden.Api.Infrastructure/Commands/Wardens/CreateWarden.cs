using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Wardens
{
    public class CreateWarden : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public string OrganizationId { get; set; }
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
                FeatureType.AddWarden, async () => await _wardenService.CreateWardenAsync(command.OrganizationId,
                    command.AuthenticatedUserId, command.Name));
        }
    }
}