using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationDeleted : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid Id { get; }
        public string UserId { get; }

        protected OrganizationDeleted()
        {
        }

        public OrganizationDeleted(Guid commandId, Guid id, string userId)
        {
            CommandId = commandId;
            Id = id;
            UserId = userId;
        }
    }
}