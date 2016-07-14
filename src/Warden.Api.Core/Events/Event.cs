using System;

namespace Warden.Api.Core.Events
{
    public abstract class Event : IEvent
    {
        public Guid Id { get; }

        protected Event()
        {
        }

        protected Event(Guid id)
        {
            Id = id;
        }
    }
}