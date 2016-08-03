using System.Threading.Tasks;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.DTO.Users;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string externalUserId);
    }
}