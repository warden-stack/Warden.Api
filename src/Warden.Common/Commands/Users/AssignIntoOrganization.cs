using System;

namespace Warden.Common.Commands.Users
{
    public class AssignIntoOrganization : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}