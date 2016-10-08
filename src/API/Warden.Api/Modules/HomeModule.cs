using Warden.Api.Core.Commands;

namespace Warden.Api.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            Get("/", args => $"Warden API is running on: {Context.Request.Url}");
        }
    }
}