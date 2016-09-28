using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.WardenChecks;

namespace Warden.Api.Modules
{
    public class WardenChecksModule : AuthenticatedModule
    {
        public WardenChecksModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, 
                  modulePath: "organizations/{organizationId}/wardens/{wardenId}/checks")
        {
            Post("/", async args =>
            {
                var command = BindAuthenticatedCommand<RequestProcessWardenCheckResult>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}