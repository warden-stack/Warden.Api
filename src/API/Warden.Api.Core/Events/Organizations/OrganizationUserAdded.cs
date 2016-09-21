using System;

namespace Warden.Api.Core.Events.Organizations
{
    public class OrganizationUserAdded : IEvent
    {
        public Guid OrganizationId { get; }
        public Guid UserId { get; }

        public OrganizationUserAdded(Guid organizationId, Guid userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}