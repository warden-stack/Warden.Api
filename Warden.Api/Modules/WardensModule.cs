using Nancy;
using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Messages.Commands.Organizations;

namespace Warden.Api.Modules
{
    public class WardensModule : ModuleBase
    {
        public WardensModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider,
                modulePath: "organizations/{organizationId}/wardens")
        {
            Post("", async args => await For<RequestNewWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessAccepted($"organizations/{args.organizationId}/wardens/" + "{0}")
                .DispatchAsync());

            Post("external", async args => await For<CreateExternalWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessAccepted($"organizations/{args.organizationId}/wardens/" + "{0}")
                .DispatchAsync());

            Delete("{wardenId}", async args => await For<DeleteWarden>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}