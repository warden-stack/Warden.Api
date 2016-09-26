using System.Threading.Tasks;
using Warden.Common.DTO.Users;

namespace Warden.Services.Users.Auth0
{
    public interface IAuth0RestClient
    {
        Task<Auth0UserDto> GetUserAsync(string externalId);
        Task<Auth0UserDto> GetUserByAccessTokenAsync(string accessToken);
    }
}