using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;

namespace Warden.Services.Storage.Repositories
{
    public interface IApiKeyRepository
    {
        Task<Maybe<ApiKeyDto>> GetAsync(string key);
        Task<Maybe<IEnumerable<ApiKeyDto>>> BrowseAsync(string userId);
        Task AddManyAsync(IEnumerable<ApiKeyDto> apiKeys);
        Task AddAsync(ApiKeyDto apiKey);
        Task DeleteAsync(string key);
    }
}