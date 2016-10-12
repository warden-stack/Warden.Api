using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCheckResultProcessed : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public object Result { get; }
        public DateTime CreatedAt { get; }

        protected WardenCheckResultProcessed()
        {
        }

        public WardenCheckResultProcessed(Guid requestId, 
            string userId,
            Guid organizationId,
            Guid wardenId,
            object result, 
            DateTime createdAt)
        {
            RequestId = requestId;
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
            CreatedAt = createdAt;
        }
    }
}