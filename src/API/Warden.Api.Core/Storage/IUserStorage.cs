using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public interface IUserStorage
    {
        Task<Maybe<UserDto>> GetAsync(string id);
    }
}