using System;

namespace Warden.Api.Core.Events.Organizations
{
    public class OrganizationUserRemoved : IEvent
    {
        public Guid OrganizationId { get; }
        public Guid UserId { get; }

        public OrganizationUserRemoved(Guid organizationId, Guid userId)
        {
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}