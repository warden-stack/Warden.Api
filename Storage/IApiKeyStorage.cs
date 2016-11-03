using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;

namespace Warden.Api.Storage
{
    public interface IApiKeyStorage
    {
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
    }
}