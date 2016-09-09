using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("organizations/{organizationId}/wardens")]
    public class WardensController : ControllerBase
    {
        public WardensController(ICommandDispatcher commandDispatcher, 
            IMapper mapper, 
            IUserService userService) 
            : base(commandDispatcher, mapper, userService)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task Post(string organizationId, [FromBody] CreateWarden request) =>
            await For(request)
                .ExecuteAsync(async c =>
                {
                    var user = await GetCurrentUser();
                    c.AuthenticatedUserId = user.Id;
                    c.OrganizationId = organizationId;

                    await CommandDispatcher.DispatchAsync(c);
                })
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(201))
                .HandleAsync();
    }
}