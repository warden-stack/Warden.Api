using System;

namespace Warden.Api.Core.Events.Organizations
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