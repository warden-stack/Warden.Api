using System;
using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;

namespace Warden.Common.Events.Users
{
    public class NewUserSignedIn : IEvent
    {
        public string UserId { get; }
        public string Email { get; }
        public Role Role { get; }
        public State State { get; }
        public DateTime CreatedAt { get; }
        public string Picture { get; }

        protected NewUserSignedIn(string picture)
        {
            Picture = picture;
        }

        public NewUserSignedIn(string userId, string email,
            Role role, State state, DateTime createdAt, 
            string picture = null)
        {
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
            CreatedAt = createdAt;
            Picture = picture;
        }
    }
}