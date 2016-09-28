using AutoMapper;
using Nancy.ModelBinding;
using Warden.Api.Core.Commands;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.Users;

namespace Warden.Api.Modules
{
    public class UserModule : AuthenticatedModule
    {
        public UserModule(ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher, modulePath: "users")
        {
            Post("/", async args =>
            {
                var command = this.Bind<SignInUser>();
                await CommandDispatcher.DispatchAsync(command);
            });

            Put("/assign", async args =>
            {
                var command = BindAuthenticatedCommand<AssignIntoOrganization>();
                await CommandDispatcher.DispatchAsync(command);
            });

            Put("/unassign", async args =>
            {
                var command = BindAuthenticatedCommand<UnassignFromOrganization>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}