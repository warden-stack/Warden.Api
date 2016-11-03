using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Api.Storage
{
    public interface IUserStorage
    {
        Task<Maybe<UserDto>> GetAsync(string id);
    }
}