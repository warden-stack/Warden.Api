using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Modules
{
    public class WardenSpawnModule : AuthenticatedModule
    {
        public WardenSpawnModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, modulePath: "warden/spawn")
        {
            Post("/", async args =>
            {
                var command = BindAuthenticatedCommand<SpawnWarden>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}