using Nancy.Security;
using Warden.Api.Core.Commands;

namespace Warden.Api.Modules.Base
{
    public class AuthenticatedModule : ModuleBase
    {
        public AuthenticatedModule(ICommandDispatcher commandDispatcher, string modulePath = "")
            : base(commandDispatcher, modulePath)
        {
            this.RequiresAuthentication();
        }
    }
}