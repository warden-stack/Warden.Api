using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Storage
{
    public interface IUserStorage
    {
        Task<Maybe<UserDto>> GetAsync(string id);
    }
}