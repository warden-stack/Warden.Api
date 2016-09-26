namespace Warden.Common.Events.Users
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Id { get; }

        public UserCreated(string email, string id)
        {
            Email = email;
            Id = id;
        }
    }
}