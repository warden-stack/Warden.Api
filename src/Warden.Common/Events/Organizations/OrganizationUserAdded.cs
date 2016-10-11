using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationUserAdded : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; }

        protected OrganizationUserAdded()
        {           
        }

        public OrganizationUserAdded(Guid commandId, Guid organizationId, string userId)
        {
            CommandId = commandId;
            OrganizationId = organizationId;
            UserId = userId;
        }
    }
}