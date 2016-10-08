using Warden.Api.Core.Commands;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Modules
{
    public class WardenSpawnModule : ModuleBase
    {
        public WardenSpawnModule(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher, modulePath: "warden-spawn")
        {
            Post("/", async args => await For<SpawnWarden>()
                .SetResourceId(x => x.WardenSpawnId)
                .OnSuccessCreated("warden-spawn/{0}")
                .DispatchAsync());
        }
    }
}