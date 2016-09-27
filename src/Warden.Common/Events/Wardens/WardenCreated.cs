using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCreated : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }

        protected WardenCreated()
        {
        }
        
        public WardenCreated(string userId, 
            Guid organizationId, 
            Guid wardenId)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
        }
    }
}