using Nancy;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.Commands.Organizations;

namespace Warden.Api.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, identityProvider, modulePath: "organizations")
        {
            Post("", async args => await For<RequestCreateOrganization>()
                .SetResourceId(x => x.OrganizationId)
                .OnSuccessCreated("organizations/{0}")
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