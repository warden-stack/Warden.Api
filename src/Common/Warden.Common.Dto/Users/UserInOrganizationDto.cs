using System;

namespace Warden.Common.Dto.Users
{
    public class UserInOrganizationDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}