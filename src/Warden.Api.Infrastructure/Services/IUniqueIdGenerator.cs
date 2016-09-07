using System;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUniqueIdGenerator
    {
        string GenerateId();
    }

    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        public string GenerateId()
        {
            var guid = Guid.NewGuid();
            var guidString = Convert.ToBase64String(guid.ToByteArray());

            return guidString.Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
        }
    }
}