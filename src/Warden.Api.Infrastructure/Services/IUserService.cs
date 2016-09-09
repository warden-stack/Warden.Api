using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Users;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserService
    {
        Task SignInUserAsync(string email, string externalId, string picture); 
        Task<UserDto> GetAsync(string externalId);
        Task<UserDto> GetByEmailAsync(string email);
        Task CreateAsync(string email, string externalId);
    }
}