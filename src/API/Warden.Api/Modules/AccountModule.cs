using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Common.Commands.Users;

namespace Warden.Api.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, identityProvider, modulePath: "")
        {
            Post("sign-in", async args => await For<SignInUser>()
                .DispatchAsync());
        }
    }
}