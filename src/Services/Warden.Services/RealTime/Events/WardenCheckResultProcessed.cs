using System;
using Warden.Common.Dto.Wardens;
using Warden.Services.RealTime.Commands;

namespace Warden.Services.RealTime.Events
{
    public class WardenCheckResultProcessed : IEvent
    {
        public Guid UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public WardenCheckResultDto Result { get; }

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