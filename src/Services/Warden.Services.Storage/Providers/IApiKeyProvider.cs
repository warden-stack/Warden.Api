using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Providers
{
    public interface IApiKeyProvider
    {
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
    }
}