namespace Warden.Common.Events.Users
{
    public class UserSignedIn : IEvent
    {
        public string Id { get; }

        public UserSignedIn(string id)
        {
            Id = id;
        }
    }
}