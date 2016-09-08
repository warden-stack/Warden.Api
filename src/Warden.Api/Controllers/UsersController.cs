using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Users;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public UsersController(ICommandDispatcher commandDispatcher, 
            IMapper mapper, 
            IUserService userService) : 
            base(commandDispatcher, mapper, userService)
        {
        }

        // POST users/register
        [Authorize]
        [HttpPost("register")]
        public async Task Register([FromBody] RegisterUser request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();

        // POST users/assign
        [Authorize]
        [HttpPost("assign")]
        public async Task AssignIntoOrganization([FromBody] AssignIntoOrganization request) =>
            await For(request)
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}