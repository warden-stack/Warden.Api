using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public class WardenSpawnController : ControllerBase
    {
        public WardenSpawnController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService)
            : base(commandDispatcher, mapper, userService)
        {
        }

        [HttpPost]
        public async Task Post([FromBody] SpawnWarden request) =>
            await For(MapTo<SpawnWarden>(request))
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}