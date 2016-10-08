using Warden.Api.Core.Commands;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Modules
{
    public class WardensModule : ModuleBase
    {
        public WardensModule(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher, modulePath: "organizations/{organizationId}/wardens")
        {
            Post("/", async args => await For<RequestCreateWarden>()
                .SetResourceId(x => x.WardenId)
                .OnSuccessCreated("organizations/{organizationId}/wardens/{0}")
                .DispatchAsync());
        }
    }
}