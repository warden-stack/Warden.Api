namespace Warden.Api.Core.Cache
{
    public interface ICacheKeys
    {
        string Users(string id);
        string ApiKeys(string apiKey);
    }
}