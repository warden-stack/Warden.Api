using System;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Commands.Wardens
{
    public class ProcessWardenCheckResult : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public WardenCheckResultDto Result { get; }

        protected ProcessWardenCheckResult()
        {
        }

        public ProcessWardenCheckResult(string userId,
            Guid organizationId,
            Guid wardenId,
            WardenCheckResultDto result)
        {
            AuthenticatedUserId = userId;
            OrganizationId = organizationId;
            WardenId = wardenId;
            Result = result;
        }
    }
}