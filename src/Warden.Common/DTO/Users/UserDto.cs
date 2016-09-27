using System;
using Warden.Common.DTO.Common;

namespace Warden.Common.DTO.Users
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}