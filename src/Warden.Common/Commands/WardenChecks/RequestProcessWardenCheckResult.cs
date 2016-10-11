using System;

namespace Warden.Common.Commands.WardenChecks
{
    public class RequestProcessWardenCheckResult : IFeatureRequestCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public object Check { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}