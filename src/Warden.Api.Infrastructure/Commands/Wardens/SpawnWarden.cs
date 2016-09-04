using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Wardens
{
    public class SpawnWarden : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public object Configuration { get; set; }
        public string Region { get; set; }
    }

    public class SpawnWardenHandler : ICommandHandler<SpawnWarden>
    {
        private readonly IWardenService _wardenService;
        private readonly IWardenConfigurationService _wardenConfigurationService;
        private readonly ISecuredRequestService _securedRequestService;
        private readonly IBus _bus;

        public SpawnWardenHandler(IWardenService wardenService, 
            IWardenConfigurationService wardenConfigurationService,
            ISecuredRequestService securedRequestService,
            IBus bus)
        {
            _wardenService = wardenService;
            _wardenConfigurationService = wardenConfigurationService;
            _securedRequestService = securedRequestService;
            _bus = bus;
        }

        public async Task HandleAsync(SpawnWarden command)
        {
            var configurationId = Guid.NewGuid();
            var resourceType = ResourceType.WardenConfiguration;
            await _wardenConfigurationService.CreateAsync(configurationId, command.Configuration);
            await _securedRequestService.CreateAsync(ResourceType.WardenConfiguration, configurationId);
            var securedToken = await _securedRequestService.GetAsync(resourceType, configurationId);
            await _bus.Publish(new Bus.Commands.CreateWarden(command.AuthenticatedUserId.ToString(),
                configurationId.ToString(), securedToken.Value.Token, command.Region));
        }
    }
}