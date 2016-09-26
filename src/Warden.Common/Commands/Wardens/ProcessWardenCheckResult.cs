using System;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Commands.Wardens
{
    public class ProcessWardenCheckResult : ICommand
    {
        public Guid UserId { get; }
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }
        public WardenCheckResultDto Result { get; }

        protected ProcessWardenCheckResult()
        {
        }

        public ProcessWardenCheckResult(Guid userId,
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