using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.Commands.Users;

namespace Warden.Api.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider) 
            : base(commandDispatcher, identityProvider, modulePath: "users")
        {
            Put("assign", async args => await For<AssignIntoOrganization>()
                .DispatchAsync());

            Put("unassign", async args => await For<UnassignFromOrganization>()
                .DispatchAsync());
        }
    }
}