using System;
using Warden.Common.DTO.Organizations;

namespace Warden.Common.DTO.Users
{
    public class UserInOrganizationDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public OrganizationRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}