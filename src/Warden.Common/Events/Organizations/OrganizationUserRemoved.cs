using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserRemoved : IEvent
    {
        public Guid OrganizationId { get; }
        public string UserId { get; }

        public OrganizationUserRemoved(Guid organizationId, string userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}