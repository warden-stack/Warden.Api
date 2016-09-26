using System.Threading.Tasks;
using Warden.Api.Infrastructure.Auth0;
using Warden.Common.DTO.Users;

namespace Warden.Api.Infrastructure.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly IAuth0RestClient _auth0RestClient;

        public UserProvider(IAuth0RestClient auth0RestClient)
        {
            _auth0RestClient = auth0RestClient;
        }

        //TODO: Fetch from the cache.
        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _auth0RestClient.GetUserAsync(id);

            return new UserDto
            {
                Id = user.UserId,
                Email = user.Email
            };
        }
    }
}