using Nancy;
using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Messages.Commands.Organizations;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Api.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IOrganizationStorage organizationStorage)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "organizations")
        {
            Get("{id}", async args => await Fetch<GetOrganization, Organization>
                (async x => await organizationStorage.GetAsync(x.UserId, x.Id))
                .HandleAsync());

            Post("", async args => await For<RequestNewOrganization>()
                .SetResourceId(x => x.OrganizationId)
                .OnSuccessAccepted("organizations/{0}")
                .DispatchAsync());

            Put("{id}", async args => await For<UpdateOrganization>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());

            Delete("{id}", async args => await For<DeleteOrganization>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}