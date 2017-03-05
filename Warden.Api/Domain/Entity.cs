using System;
using System.Collections.Generic;
using Warden.Messages.Events;

namespace Warden.Api.Domain
{
    public abstract class Entity : IEntity
    {
        private readonly Dictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

        public IEnumerable<IEvent> Events => _events.Values;

        protected void AddEvent(IEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
