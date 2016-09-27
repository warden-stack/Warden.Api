namespace Warden.Common.Events.ApiKeys
{
    public class ApiKeyCreated : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public string ApiKey { get; }

        protected ApiKeyCreated()
        {
        }

        public ApiKeyCreated(string userId, string apiKey)
        {
            UserId = userId;
            ApiKey = apiKey;
        }
    }
}