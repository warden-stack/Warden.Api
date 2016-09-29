namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public string Email { get; }
        public string Role { get; }
        public string State { get; }

        protected UserSignedIn()
        {
        }

        public UserSignedIn(string userId, string email, string role, string state)
        {
            UserId = userId;
            Email = email;
            Role = role;
            State = state;
        }
    }
}