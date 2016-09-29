using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCreated : IAuthenticatedEvent
    {
        public Guid WardenId { get; }
        public string Name { get; }
        public Guid OrganizationId { get; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; }
        public bool Enabled { get;}

        protected WardenCreated()
        {
        }
        
        public WardenCreated(Guid wardenId,
            string name,
            Guid organizationId, 
            string userId, 
            DateTime createdAt, 
            bool enabled)
        {
            WardenId = wardenId;
            Name = name;
            OrganizationId = organizationId;
            UserId = userId;
            CreatedAt = createdAt;
            Enabled = enabled;
        }
    }
}