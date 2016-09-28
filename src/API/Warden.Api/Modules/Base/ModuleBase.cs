using Nancy;
using Warden.Api.Core.Commands;

namespace Warden.Api.Modules.Base
{
    public class ModuleBase : NancyModule
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public ModuleBase(ICommandDispatcher commandDispatcher, string modulePath = "")
            : base(modulePath)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}