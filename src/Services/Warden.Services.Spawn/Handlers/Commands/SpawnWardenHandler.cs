//using System;
//using System.Threading.Tasks;
//using Warden.Api.Core.Domain.Security;
//using Warden.Api.Core.Services;
//using Warden.Common.Commands;
//using Warden.Common.Commands.Wardens;

//namespace Warden.Api.Core.Commands.Handlers
//{
//    public class SpawnWardenHandler : ICommandHandler<SpawnWarden>
//    {
//        private readonly IWardenService _wardenService;
//        private readonly IWardenConfigurationService _wardenConfigurationService;
//        private readonly ISecuredRequestService _securedRequestService;

//        public SpawnWardenHandler(IWardenService wardenService, 
//            IWardenConfigurationService wardenConfigurationService,
//            ISecuredRequestService securedRequestService)
//        {
//            _wardenService = wardenService;
//            _wardenConfigurationService = wardenConfigurationService;
//            _securedRequestService = securedRequestService;
//        }

//        public async Task HandleAsync(SpawnWarden command)
//        {
//            var securedRequestId = Guid.NewGuid();
//            var configurationId = Guid.NewGuid();
//            await _wardenConfigurationService.CreateAsync(configurationId, command.Configuration);
//            await _securedRequestService.CreateAsync(securedRequestId, ResourceType.WardenConfiguration, configurationId);
//            var securedRequest = await _securedRequestService.GetAsync(securedRequestId);
//            //await _bus.Publish(new Common.Commands.SpawnWarden(command.AuthenticatedUserId,
//            //    configurationId.ToString(), securedRequest.Value.Token, command.Region));
//        }
//    }
//}