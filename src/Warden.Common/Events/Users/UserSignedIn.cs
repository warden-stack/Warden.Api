using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;

namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public string Email { get; }
        public Role Role { get; }

        protected UserSignedIn()
        {
        }

        public UserSignedIn(string userId, string email, Role role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}