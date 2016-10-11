using System;

namespace Warden.Common.Events.Users
{
    public class NewUserSignedIn : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public string UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string State { get; }
        public DateTime CreatedAt { get; }
        public string PictureUrl { get; }

        protected NewUserSignedIn()
        {
        }

        public NewUserSignedIn(Guid commandId, 
            string userId, string email,
            string role, string state, DateTime createdAt, 
            string pictureUrl = null)
        {
            CommandId = commandId;
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
            CreatedAt = createdAt;
            PictureUrl = pictureUrl;
        }
    }
}