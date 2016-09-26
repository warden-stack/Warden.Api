using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUpdated : IEvent
    {
        public Guid Id { get; }

        public OrganizationUpdated(Guid id)
        {
            Id = id;
        }
    }
}