namespace Warden.Api.Core.Services
{
    public interface IIdentityProvider
    {
        string GetUserIdForApiKey(string apiKey);
        void SetUserIdForApiKey(string apiKey, string userId);
    }
}