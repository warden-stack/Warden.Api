using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserRemoved : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; }

        protected OrganizationUserRemoved()
        {           
        }

        public OrganizationUserRemoved(Guid commandId, Guid organizationId, string userId)
        {
            CommandId = commandId;
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}