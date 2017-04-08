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
            : base(commandDispatcher, validatorResolver, identityProvider,
                modulePath: "organizations/{organizationId}/wardens/{wardenId}/manager")
        {
            Post("start", async args => await For<StartWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());

            Post("pause", async args => await For<PauseWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());

            Post("stop", async args => await For<StopWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());

            Post("ping", async args => await For<PingWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());

            Delete("", async args => await For<KillWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());
        }
    }
}