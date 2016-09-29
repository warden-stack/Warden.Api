using System;

namespace Warden.Services.WardenChecks.Domain
{
    public class WardenCheckResultStorage
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public WardenCheckResult Result { get; set; }
    }
}