using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationCreated : IEvent
    {
        public Guid Id { get; }

        public OrganizationCreated(Guid id)
        {
            Id = id;
        }
    }
}