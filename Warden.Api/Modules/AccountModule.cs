using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Services.Users.Shared.Commands;

namespace Warden.Api.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "")
        {
            Post("sign-up", async args => await For<SignUp>()
                .DispatchAsync());

            Post("sign-in", async args => await For<SignIn>()
                .DispatchAsync());

            Post("sign-out", async args => await For<SignOut>()
                .DispatchAsync());
        }
    }
}