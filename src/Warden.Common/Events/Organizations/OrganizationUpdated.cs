using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUpdated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public Guid Id { get; }
        public string UserId { get; }

        protected OrganizationUpdated()
        {
        }

        public OrganizationUpdated(Guid requestId, Guid id, string userId)
        {
            RequestId = requestId;
            Id = id;
            UserId = userId;
        }
    }
}