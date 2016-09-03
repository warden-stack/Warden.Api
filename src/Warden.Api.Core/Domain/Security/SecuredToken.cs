using System;
using System.Security.Cryptography;

namespace Warden.Api.Core.Domain.Security
{
    public class SecuredToken : ValueObject<SecuredToken>
    {
        private static readonly string[] ReplaceableCharacters = {"+", "?", "&"};

        public string Token { get; protected set; }

        protected SecuredToken()
        {
        }

        public static SecuredToken Create()
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

                return new SecuredToken
                {
                    Token = token
                };
            }
        }

        protected override bool EqualsCore(SecuredToken other)
        {
            return Token.Equals(other.Token);
        }

        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}