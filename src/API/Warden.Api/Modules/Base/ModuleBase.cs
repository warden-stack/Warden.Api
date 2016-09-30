using System.Collections.Generic;
using Nancy;
using Warden.Api.Core.Commands;
using Warden.Common.Types;

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

        //TODO: Add headers etc.
        protected IEnumerable<T> FromPagedResult<T>(Maybe<PagedResult<T>> result)
        {
            return result.HasValue ? result.Value.Items : new List<T>();
        }
    }
}