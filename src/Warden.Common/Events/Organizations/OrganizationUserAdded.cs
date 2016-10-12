using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserAdded : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; }

        protected OrganizationUserAdded()
        {           
        }

        public OrganizationUserAdded(Guid requestId, Guid organizationId, string userId)
        {
            RequestId = requestId;
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}