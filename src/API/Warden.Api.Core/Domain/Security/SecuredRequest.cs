using System;
using System.Security.Cryptography;

namespace Warden.Api.Core.Domain.Security
{
    public class SecuredRequest : IdentifiableEntity
    {
        private static readonly string[] ReplaceableCharacters = {"+", "?", "&", "%"};
        public ResourceType ResourceType { get; protected set; }
        public Guid ResourceId { get; protected set; }
        public string Token { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UsedAt { get; protected set; }

        protected SecuredRequest()
        {
        }

        public SecuredRequest(Guid id, ResourceType resourceType, Guid resourceId)
        {
            Id = id;
            ResourceType = resourceType;
            ResourceId = resourceId;
            CreatedAt = DateTime.UtcNow;
            Token = CreateToken();
        }

        public void Consume(string token)
        {
            if (UsedAt.HasValue)
                throw new InvalidOperationException("Token has been already used.");
            if(!Token.Equals(token))
                throw new InvalidOperationException("Invalid token.");

            UsedAt = DateTime.UtcNow;
        }

        private static string CreateToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                var token = Convert.ToBase64String(tokenData);
                foreach (var replaceableCharacter in ReplaceableCharacters)
                {
                    token = token.Replace(replaceableCharacter, string.Empty);
                }

                return token;
            }
        }
    }
}