using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.DTO.WardenConfigurations;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public class WardenConfigurationsController : ControllerBase
    {
        public WardenConfigurationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService)
            : base(commandDispatcher, mapper, userService)
        {
        }

        [HttpGet]
        public async Task<WardenConfigurationDto> Get(string id, string token) =>
            await Task.FromResult(new WardenConfigurationDto
            {
                Configuration = "test"
            });
    }
}