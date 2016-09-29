using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Services.Storage.Repositories
{
    public interface IUserRepository
    {
        Task<Maybe<UserDto>> GetByIdAsync(string id);
        Task<Maybe<UserDto>> GetByEmailAsync(string email);
        Task AddAsync(UserDto user);
        Task UpdateAsync(UserDto user);
    }
}