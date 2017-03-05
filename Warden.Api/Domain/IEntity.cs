using System.Collections.Generic;
using Warden.Messages.Events;

namespace Warden.Api.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}