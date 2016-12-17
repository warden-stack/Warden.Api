using Nancy;
using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Services.Organizations.Shared.Commands;

namespace Warden.Api.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "organizations")
        {
            Post("", async args => await For<RequestNewOrganization>()
                .SetResourceId(x => x.OrganizationId)
                .OnSuccessAccepted("organizations/{0}")
                .DispatchAsync());

            Put("{id}", async args => await For<EditOrganization>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());

            Delete("{id}", async args => await For<DeleteOrganization>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}