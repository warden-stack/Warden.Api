using Nancy;
using Nancy.ModelBinding;
using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.WardenChecks;

namespace Warden.Api.Modules
{
    public class WardenChecksModule : ModuleBase
    {
        public WardenChecksModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, 
                  modulePath: "organizations/{organizationId}/wardens/{wardenId}/checks")
        {
            Post("/", async args =>
            {
                var command = this.Bind<RequestProcessWardenCheckResult>();
                command.UserId = "57d068eaf78ad35973d0a747";
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}