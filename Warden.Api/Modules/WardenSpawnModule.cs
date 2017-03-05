using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Messages.Commands.Spawn;

namespace Warden.Api.Modules
{
    public class WardenSpawnModule : ModuleBase
    {
        public WardenSpawnModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "warden-spawn")
        {
            Post("", async args => await For<SpawnWarden>()
                .SetResourceId(x => x.WardenSpawnId)
                .OnSuccessAccepted("warden-spawn/{0}")
                .DispatchAsync());
        }
    }
}