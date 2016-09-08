namespace Warden.Api.Core.Events.Users
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string ExternalId { get; }

        public UserCreated(string email, string externalId)
        {
            Email = email;
            ExternalId = externalId;
        }
    }
}