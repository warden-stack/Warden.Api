using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserAdded : IEvent
    {
        public Guid OrganizationId { get; }
        public string UserId { get; }

        public OrganizationUserAdded(Guid organizationId, string userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}