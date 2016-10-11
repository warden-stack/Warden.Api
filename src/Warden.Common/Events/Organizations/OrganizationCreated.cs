using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationCreated : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid OrganizationId { get; }
        public string Name { get; }
        public string Description { get; }
        public string UserId { get; }
        public string UserEmail { get; }
        public string UserOrganizationRole { get; }
        public DateTime UserCreatedAt { get; }

        protected OrganizationCreated()
        {
        }
        
        public OrganizationCreated(Guid commandId, Guid organizationId, string name, string description,
            string userId, string userEmail, string userOrganizationRole, DateTime userCreatedAt)
        {
            CommandId = commandId;
            OrganizationId = organizationId;
            Name = name;
            Description = description;
            UserId = userId;
            UserEmail = userEmail;
            UserOrganizationRole = userOrganizationRole;
            UserCreatedAt = userCreatedAt;
        }
    }
}