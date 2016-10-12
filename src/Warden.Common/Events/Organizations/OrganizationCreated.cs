using System;

namespace Warden.Common.Events.Organizations
{
    public class OrganizationCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
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
        
        public OrganizationCreated(Guid requestId, Guid organizationId, string name, string description,
            string userId, string userEmail, string userOrganizationRole, DateTime userCreatedAt)
        {
            RequestId = requestId;
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