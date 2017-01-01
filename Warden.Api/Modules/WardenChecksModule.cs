using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Services.WardenChecks.Shared.Commands;

namespace Warden.Api.Modules
{
    public class WardenChecksModule : ModuleBase
    {
        public WardenChecksModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider,
                modulePath: "organizations/{organizationId}/wardens/{wardenId}/checks")
        {
            Post("", async args => await For<RequestProcessWardenCheckResult>()
                .SetResourceId(x => x.ResultId)
                .OnSuccessAccepted($"organizations/{args.organizationId}/wardens/{args.wardenId}/checks/" + "{0}")
                .DispatchAsync());
        }
    }
}