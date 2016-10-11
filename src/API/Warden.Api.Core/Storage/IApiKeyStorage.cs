using System.Threading.Tasks;
using Warden.Api.Core.Filters;
using Warden.Api.Core.Queries;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;

namespace Warden.Api.Core.Storage
{
    public interface IApiKeyStorage
    {
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
    }
}