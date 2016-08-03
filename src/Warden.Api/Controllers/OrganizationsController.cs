using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Organizations;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.DTO;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public class OrganizationsController : ControllerBase
    {
        public OrganizationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService) 
            : base(commandDispatcher, mapper, userService)
        {
        }

        [HttpPost]
        public async Task Post([FromBody] CreateOrganization request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}