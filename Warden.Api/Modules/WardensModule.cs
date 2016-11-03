using Nancy;
using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Modules
{
    public class WardensModule : ModuleBase
    {
        public WardensModule(ICommandDispatcher commandDispatcher, 
            IIdentityProvider identityProvider)
            : base(commandDispatcher, identityProvider, modulePath: "organizations/{organizationId}/wardens")
        {
            Post("", async args => await For<RequestNewWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessAccepted("organizations/{organizationId}/wardens/{0}")
                .DispatchAsync());

            Delete("{wardenId}", async args => await For<DeleteWarden>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}