using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Common.Dto.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.WardenChecks
{
    public class SaveWardenCheck : ICommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public WardenCheckResultDto Check { get; set; }
    }

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