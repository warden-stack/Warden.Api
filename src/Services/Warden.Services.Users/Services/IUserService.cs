using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Services.Users.Services
{
    public interface IUserService
    {
        Task<Maybe<UserDto>> GetAsync(string id);
        Task CreateAsync(string email, string id, bool activate = true);
    }
}