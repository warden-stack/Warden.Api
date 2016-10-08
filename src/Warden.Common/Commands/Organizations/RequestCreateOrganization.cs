using System;

namespace Warden.Common.Commands.Organizations
{
    public class RequestCreateOrganization : IFeatureRequestCommand
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}