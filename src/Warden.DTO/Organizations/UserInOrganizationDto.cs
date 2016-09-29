using System;

namespace Warden.DTO.Organizations
{
    public class UserInOrganizationDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}