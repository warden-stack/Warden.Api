using System.Collections.Generic;
using Warden.Common.Events;

namespace Warden.Services.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}