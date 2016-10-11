using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUpdated : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid Id { get; }
        public string UserId { get; }

        protected OrganizationUpdated()
        {
        }

        public OrganizationUpdated(Guid commandId, Guid id, string userId)
        {
            CommandId = commandId;
            Id = id;
            UserId = userId;
        }
    }
}