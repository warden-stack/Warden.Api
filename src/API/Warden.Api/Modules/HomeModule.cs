using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;

namespace Warden.Api.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider)
            : base(commandDispatcher, identityProvider)
        {
            Get("", args => $"Warden API is running on: {Context.Request.Url}");
        }
    }
}