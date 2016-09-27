using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Api.Core.Cache
{
    public interface IApiCache
    {
        Task<Maybe<UserDto>> GetUserAsync(string id);
        Task SetUserAsync(string id, UserDto user);
        Task SetUserIdForApiKeyAsync(string userId, string apiKey);
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
    }
}