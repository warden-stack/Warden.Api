using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public Guid WardenId { get; }
        public string Name { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; }
        public DateTime CreatedAt { get; }
        public bool Enabled { get;}

        protected WardenCreated()
        {
        }
        
        public WardenCreated(Guid requestId, 
            Guid wardenId,
            string name,
            Guid organizationId, 
            string userId, 
            DateTime createdAt, 
            bool enabled)
        {
            RequestId = requestId;
            WardenId = wardenId;
            Name = name;
            OrganizationId = organizationId;
            UserId = userId;
            CreatedAt = createdAt;
            Enabled = enabled;
        }
    }
}