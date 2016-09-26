using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Infrastructure.Settings;
using Warden.Common.Extensions;

namespace Warden.Api.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private readonly string _key;
        private const int MinSecureKeySize = 40;
        private const int MaxSecureKeySize = 60;
        private static readonly Random Random = new Random();

        public Encrypter(GeneralSettings settings)
        {
            if (settings.EncrypterKey.Empty())
                throw new DomainException("Encrypter key can not be empty.");

            _key = settings.EncrypterKey;
        }

        public string GetRandomSecureKey()
        {
            var size = Random.Next(MinSecureKeySize, MaxSecureKeySize);
            var bytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);

                return Convert.ToBase64String(bytes);
            }
        }
    }
}