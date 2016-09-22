using System;
using Warden.Common.Commands;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Events
{
    public class WardenCheckResultProcessed : IEvent
    {
        public Guid UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public WardenCheckResultDto Result { get; }

        protected WardenCheckResultProcessed()
        {
        }

        public WardenCheckResultProcessed(Guid userId,
            Guid organizationId,
            Guid wardenId,
            WardenCheckResultDto result)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
        }

        public WardenCheckResultProcessed(ProcessWardenCheckResult result)
        {
            UserId = result.UserId;
            OrganizationId = result.OrganizationId;
            WardenId = result.WardenId;
            Result = result.Result;
        }
    }
}