using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.Organizations;

namespace Warden.Api.Modules
{
    public class OrganizationsModule : AuthenticatedModule
    {
        public OrganizationsModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, modulePath: "organizations")
        {
            Post("/", async args =>
            {
                var command = BindAuthenticatedCommand<RequestCreateOrganization>();
                await CommandDispatcher.DispatchAsync(command);
            });

            Put("/{id}", async args =>
            {
                var command = BindAuthenticatedCommand<EditOrganization>();
                await CommandDispatcher.DispatchAsync(command);
            });

            Delete("/{id}", async args =>
            {
                var command = BindAuthenticatedCommand<DeleteOrganization>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}