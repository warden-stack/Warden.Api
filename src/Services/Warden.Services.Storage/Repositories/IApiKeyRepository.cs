using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;

namespace Warden.Services.Storage.Repositories
{
    public interface IApiKeyRepository
    {
        Task<IEnumerable<ApiKeyDto>> BrowseAsync(string userId);
        Task AddAsync(ApiKeyDto apiKey);
        Task DeleteAsync(string key);
    }
}