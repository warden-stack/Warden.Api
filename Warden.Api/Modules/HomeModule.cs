using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;

namespace Warden.Api.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider)
        {
          Get("", args => "Welcome to the Warden API!");
        }
    }
}