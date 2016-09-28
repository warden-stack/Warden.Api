using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Modules
{
    public class WardensModule : AuthenticatedModule
    {
        public WardensModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, modulePath: "organizations/{organizationId}/wardens")
        {
            Post("/", async args =>
            {
                var command = BindAuthenticatedCommand<RequestCreateWarden>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}