using System;

namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string State { get; }

        protected UserSignedIn()
        {
        }

        public UserSignedIn(Guid requestId, string userId, string email, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
        }
    }
}