using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Controllers
{
    [Route("warden/spawn")]
    public class WardenSpawnController : ControllerBase
    {
        public WardenSpawnController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserProvider userProvider)
            : base(commandDispatcher, mapper, userProvider)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] object request) =>
            await For(MapTo<SpawnWarden>(request))
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}