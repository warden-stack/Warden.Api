namespace Warden.Api.Core.Cache
{
    public class CacheKeys : ICacheKeys
    {
        public string Users(string id) => $"users:{id}";
        public string ApiKeys(string apiKey) => $"api-keys:{apiKey}";
    }
}