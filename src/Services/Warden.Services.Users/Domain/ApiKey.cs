using System;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Users.Domain
{
    public class ApiKey : Entity, ITimestampable
    {
        public Guid Id { get; protected set; }
        public string Key { get; protected set; }
        public string UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected ApiKey()
        {
        }

        public ApiKey(Guid id, string key, string userId)
        {
            if (key.Empty())
                throw new DomainException("API key can not be empty.");
            if (userId.Empty())
                throw new DomainException("Can not create an API key without user.");

            Id = id;
            Key = key;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}