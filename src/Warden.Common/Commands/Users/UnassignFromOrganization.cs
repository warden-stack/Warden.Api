using System;

namespace Warden.Common.Commands.Users
{
    public class UnassignFromOrganization : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
    }
}