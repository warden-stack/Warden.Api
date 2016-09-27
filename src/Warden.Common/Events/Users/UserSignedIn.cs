namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IEvent
    {
        public string UserId { get; }

        protected UserSignedIn()
        {
        }

        public UserSignedIn(string userId)
        {
            UserId = userId;
        }
    }
}