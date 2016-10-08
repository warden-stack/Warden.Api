using System;

namespace Warden.Common.Commands.Wardens
{
    public class RequestCreateWarden : IFeatureRequestCommand
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Enabled { get; set; }
    }
}