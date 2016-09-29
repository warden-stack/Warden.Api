using System;

namespace Warden.Common.Events.Users
{
    public class NewUserSignedIn : IEvent
    {
        public string UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string State { get; }
        public DateTime CreatedAt { get; }
        public string PictureUrl { get; }

        protected NewUserSignedIn(string pictureUrl)
        {
            PictureUrl = pictureUrl;
        }

        public NewUserSignedIn(string userId, string email,
            string role, string state, DateTime createdAt, 
            string pictureUrl = null)
        {
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
            CreatedAt = createdAt;
            PictureUrl = pictureUrl;
        }
    }
}