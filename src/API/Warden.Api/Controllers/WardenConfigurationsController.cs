using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("warden/configurations")]
    public class WardenConfigurationsController : ControllerBase
    {
        private readonly IWardenConfigurationService _wardenConfigurationService;
        private readonly ISecuredRequestService _securedRequestService;

        public WardenConfigurationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService,
            IWardenConfigurationService wardenConfigurationService,
            ISecuredRequestService securedRequestService)
            : base(commandDispatcher, mapper, userService)
        {
            _wardenConfigurationService = wardenConfigurationService;
            _securedRequestService = securedRequestService;
        }

        [HttpGet]
        [HttpGet("{id}")]
        public async Task<object> Get(string id, string token)
        {
            var resourceId = Guid.Parse(id);
            await _securedRequestService.ConsumeAsync(ResourceType.WardenConfiguration,
                resourceId, token);
            var configuration = await _wardenConfigurationService.GetAsync(resourceId);
            if (configuration.HasValue)
                return configuration.Value.Configuration;

            Response.StatusCode = 404;

            return null;
        }
    }
}