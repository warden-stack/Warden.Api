using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.WardenChecks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("wardens/{id}/checks")]
    public class WardenChecksController : ControllerBase
    {
        public WardenChecksController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService)
            : base(commandDispatcher, mapper, userService)
        {
        }

        [HttpPost]
        public async Task Post([FromBody] SaveWardenCheck request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}