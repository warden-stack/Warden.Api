using Nancy;
using Warden.Api.Core.Commands;
using Warden.Common.Commands.WardenChecks;

namespace Warden.Api.Modules
{
    public class WardenChecksModule : ModuleBase
    {
        public WardenChecksModule(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher,
                modulePath: "organizations/{organizationId}/wardens/{wardenId}/checks")
        {
            Post("", async args => await For<RequestProcessWardenCheckResult>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}