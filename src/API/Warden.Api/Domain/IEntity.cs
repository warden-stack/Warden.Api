using System.Collections.Generic;
using Warden.Common.Events;

namespace Warden.Api.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}