using System.Collections.Concurrent;
using Warden.Common.Extensions;

namespace Warden.Api.Services
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly ConcurrentDictionary<string, string> _userIds = new ConcurrentDictionary<string, string>();

        public string GetUserIdForApiKey(string apiKey)
        {
            if (apiKey.Empty())
                return string.Empty;

            return !_userIds.ContainsKey(apiKey) ? string.Empty : _userIds[apiKey];
        }

        public void SetUserIdForApiKey(string apiKey, string userId)
        {
            if(apiKey.Empty())
                return;

            _userIds[apiKey] = userId;
        }
    }
}