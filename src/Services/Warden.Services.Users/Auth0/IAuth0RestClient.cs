using System.Threading.Tasks;
using Warden.Services.Users.Domain;

namespace Warden.Services.Users.Auth0
{
    public interface IAuth0RestClient
    {
        Task<Auth0User> GetUserAsync(string userId);
        Task<Auth0User> GetUserByAccessTokenAsync(string accessToken);
    }
}