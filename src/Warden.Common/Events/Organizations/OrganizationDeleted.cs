using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationDeleted : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public Guid Id { get; }
        public string UserId { get; }

        protected OrganizationDeleted()
        {
        }

        public OrganizationDeleted(Guid requestId, Guid id, string userId)
        {
            RequestId = requestId;
            Id = id;
            UserId = userId;
        }
    }
}