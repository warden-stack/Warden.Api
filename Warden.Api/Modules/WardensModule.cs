using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Messages.Commands.Organizations;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Api.Modules
{
    public class WardensModule : ModuleBase
    {
        public WardensModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IOrganizationStorage organizationStorage)
            : base(commandDispatcher, validatorResolver, identityProvider,
                modulePath: "organizations/{organizationId}/wardens")
        {
            Get("{wardenId}", async args => await Fetch<GetWarden, Warden.Services.Storage.Models.Organizations.Warden>
                (async x => await organizationStorage.GetWardenAsync(x.UserId, x.OrganizationId, x.WardenId))
                .HandleAsync());

            Post("", async args => await For<RequestNewWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessAccepted($"organizations/{args.organizationId}/wardens/" + "{0}")
                .DispatchAsync());

            Post("external", async args => await For<CreateExternalWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessAccepted($"organizations/{args.organizationId}/wardens/" + "{0}")
                .DispatchAsync());

            Delete("{wardenId}", async args => await For<DeleteWarden>()
                .OnSuccessAccepted()
                .DispatchAsync());
        }
    }
}