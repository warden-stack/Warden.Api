using System;

namespace Warden.Common.Events.ApiKeys
{
    public class ApiKeyCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get;}
        public string UserId { get; }
        public string ApiKey { get; }

        protected ApiKeyCreated()
        {
        }

        public ApiKeyCreated(Guid requestId, string userId, string apiKey)
        {
            RequestId = requestId;
            UserId = userId;
            ApiKey = apiKey;
        }
    }
}