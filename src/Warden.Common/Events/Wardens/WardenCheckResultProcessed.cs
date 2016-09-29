using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCheckResultProcessed : IEvent
    {
        public string UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public object Result { get; }

        protected WardenCheckResultProcessed()
        {
        }

        public WardenCheckResultProcessed(string userId,
            Guid organizationId,
            Guid wardenId,
            object result)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
        }
    }
}