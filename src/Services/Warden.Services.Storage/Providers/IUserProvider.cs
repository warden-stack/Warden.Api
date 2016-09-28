using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IUserProvider
    {
        Task<Maybe<UserDto>> GetAsync(string userId);
    }
}