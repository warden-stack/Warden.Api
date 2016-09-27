using System;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Commands.WardenChecks
{
    public class RequestProcessWardenCheckResult : IFeatureRequestCommand
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public WardenCheckResultDto Check { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}