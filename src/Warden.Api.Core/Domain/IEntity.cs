using System.Collections.Generic;
using Warden.Api.Core.Events;

namespace Warden.Api.Core.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}