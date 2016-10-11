using System;

namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public string UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string State { get; }

        protected UserSignedIn()
        {
        }

        public UserSignedIn(Guid commandId, string userId, string email, string role, string state)
        {
            CommandId = commandId;
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
        }
    }
}