using System.Collections.Generic;
using Warden.Common.Events;

namespace Warden.Common.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}