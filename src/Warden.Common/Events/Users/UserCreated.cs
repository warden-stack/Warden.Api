using System;
using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;

namespace Warden.Common.Events.Users
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string UserId { get; }
        public Role Role { get; }
        public State State { get; }
        public DateTime CreatedAt { get; }

        protected UserCreated()
        {
        }

        public UserCreated(string email, string userId, 
            Role role, State state, DateTime createdAt)
        {
            Email = email;
            UserId = userId;
            Role = role;
            State = state;
            CreatedAt = createdAt;
        }
    }
}