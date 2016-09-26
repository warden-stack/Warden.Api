using System;

namespace Warden.Api.Core.Services
{
    public interface IUniqueIdGenerator
    {
        string Create();
    }

    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        public string Create()
        {
            var guid = Guid.NewGuid();
            var guidString = Convert.ToBase64String(guid.ToByteArray());

            return guidString.Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
        }
    }
}