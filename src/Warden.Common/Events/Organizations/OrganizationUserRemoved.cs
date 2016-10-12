using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserRemoved : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; }

        protected OrganizationUserRemoved()
        {           
        }

        public OrganizationUserRemoved(Guid requestId, Guid organizationId, string userId)
        {
            RequestId = requestId;
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}