using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.Commands.Users;

namespace Warden.Api.Controllers
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public UsersController(ICommandDispatcher commandDispatcher, 
            IMapper mapper,
            IUserProvider userProvider) : 
            base(commandDispatcher, mapper, userProvider)
        {
        }

        // POST users
        [Authorize]
        [HttpPost]
        public async Task SignInAsync([FromBody] SignInUser request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();

        // PUT users/assign
        [Authorize]
        [HttpPut("assign")]
        public async Task AssignIntoOrganizationAsync([FromBody] AssignIntoOrganization request) =>
            await For(request)
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
        
        // PUT users/unassign
        [Authorize]
        [HttpPut("unassign")]
        public async Task UnassignFromOrganizationAsync([FromBody] UnassignFromOrganization request) =>
            await For(request)
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}