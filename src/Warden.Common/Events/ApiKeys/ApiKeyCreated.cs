namespace Warden.Common.Events.ApiKeys
{
    public class ApiKeyCreated : IAuthenticatedEvent
    {
        public string AuthenticatedUserId { get; set; }
        public string ApiKey { get; }

        protected ApiKeyCreated()
        {
        }

        public ApiKeyCreated(string userId, string apiKey)
        {
            AuthenticatedUserId = userId;
            ApiKey = apiKey;
        }
    }
}