using System;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Core.Domain.Users
{
    public class ApiKey : Entity, ITimestampable
    {
        public Guid Id { get; protected set; }
        public string Key { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected ApiKey()
        {
        }

        public ApiKey(Guid id, string key, Guid userId)
        {
            if (key.Empty())
                throw new DomainException("API key can not be empty.");
            if (userId.IsEmpty())
                throw new DomainException("Can not create an API key without user.");

            Id = id;
            Key = key;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}