using System;

namespace Warden.Api.Core.Events.Organizations
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