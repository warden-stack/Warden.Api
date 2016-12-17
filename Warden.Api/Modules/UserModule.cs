using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Services.Organizations.Shared.Commands;

namespace Warden.Api.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider) 
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "users")
        {
            Put("assign", async args => await For<AssignUserToOrganization>()
                .DispatchAsync());

            Put("unassign", async args => await For<UnassignUserFromOrganization>()
                .DispatchAsync());
        }
    }
}