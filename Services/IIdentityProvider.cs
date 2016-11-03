namespace Warden.Api.Services
{
    public interface IIdentityProvider
    {
        string GetUserIdForApiKey(string apiKey);
        void SetUserIdForApiKey(string apiKey, string userId);
    }
}