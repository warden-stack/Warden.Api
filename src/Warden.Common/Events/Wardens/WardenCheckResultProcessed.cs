using System;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Events.Wardens
{
    public class WardenCheckResultProcessed : IEvent
    {
        public string UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public WardenCheckResultDto Result { get; }

        protected WardenCheckResultProcessed()
        {
        }

        public WardenCheckResultProcessed(string userId,
            Guid organizationId,
            Guid wardenId,
            WardenCheckResultDto result)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
        }
    }
}