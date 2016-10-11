using System;

namespace Warden.Common.Events.ApiKeys
{
    public class ApiKeyCreated : IAuthenticatedEvent
    {
        public Guid CommandId { get;}
        public string UserId { get; }
        public string ApiKey { get; }

        protected ApiKeyCreated()
        {
        }

        public ApiKeyCreated(Guid commandId, string userId, string apiKey)
        {
            CommandId = commandId;
            UserId = userId;
            ApiKey = apiKey;
        }
    }
}