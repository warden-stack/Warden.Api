using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public class WardenConfigurationsController : ControllerBase
    {
        private readonly IWardenConfigurationService _wardenConfigurationService;

        public WardenConfigurationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService,
            IWardenConfigurationService wardenConfigurationService)
            : base(commandDispatcher, mapper, userService)
        {
            _wardenConfigurationService = wardenConfigurationService;
        }

        [HttpGet]
        [HttpGet("{id}")]
        public async Task<object> Get(string id, string token)
        {
            var configuration = await _wardenConfigurationService.GetConfigurationAsync(Guid.Parse(id), token);
            if (configuration.HasValue)
                return configuration.Value.Configuration;

            Response.StatusCode = 404;

            return null;
        }
    }
}