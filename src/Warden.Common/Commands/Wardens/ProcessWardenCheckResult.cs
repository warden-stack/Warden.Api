using System;

namespace Warden.Common.Commands.Wardens
{
    public class ProcessWardenCheckResult : IAuthenticatedCommand
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public object Result { get; }
        public DateTime CreatedAt { get; }

        protected ProcessWardenCheckResult()
        {
        }

        public ProcessWardenCheckResult(string userId,
            Guid organizationId,
            Guid wardenId,
            object result, DateTime createdAt)
        {
            UserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
            CreatedAt = createdAt;
        }
    }
}