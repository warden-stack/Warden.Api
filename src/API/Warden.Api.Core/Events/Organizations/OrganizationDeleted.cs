using System;

namespace Warden.Api.Core.Events.Organizations
{
    public class OrganizationDeleted : IEvent
    {
        public Guid Id { get; }

        public OrganizationDeleted(Guid id)
        {
            Id = id;
        }
    }
}