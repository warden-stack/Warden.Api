using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Storage
{
    public interface IApiKeyStorage
    {
        Task<Maybe<string>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
    }
}