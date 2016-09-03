using System;

namespace Warden.Api.Core.Domain.Security
{
    public class SecuredRequest : IdentifiableEntity
    {
        public SecuredResource ResourceType { get; protected set; }
        public Guid ResourceId { get; protected set; }
        public SecuredToken Token { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UsedAt { get; protected set; }

        protected SecuredRequest()
        {
        }

        public SecuredRequest(SecuredResource resourceType, Guid resourceId)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
            CreatedAt = DateTime.UtcNow;
            Token = SecuredToken.Create();
        }

        public void Consume(string token)
        {
            if (UsedAt.HasValue)
                throw new InvalidOperationException("Token has been already used.");
            if(!Token.Token.Equals(token))
                throw new InvalidOperationException("Invalid token.");

            UsedAt = DateTime.UtcNow;
        }
    }
}