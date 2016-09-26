using System.Threading.Tasks;
using Warden.Common.DTO.Users;

namespace Warden.Api.Core.Services
{
    public interface IUserService
    {
        Task SignInUserAsync(string email, string externalId, string picture); 
        Task<UserDto> GetAsync(string externalId);
        Task<UserDto> GetByAccessTokenAsync(string accessToken);
        Task<UserDto> GetByEmailAsync(string email);
        Task CreateAsync(string email, string externalId, bool activate = true);
    }
}