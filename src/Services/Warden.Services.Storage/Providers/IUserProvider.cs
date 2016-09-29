using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Services.Storage.Providers
{
    public interface IUserProvider
    {
        Task<Maybe<UserDto>> GetAsync(string userId);
    }
}