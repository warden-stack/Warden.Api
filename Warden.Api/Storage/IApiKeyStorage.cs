using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Api.Storage
{
    public interface IApiKeyStorage
    {
        Task<Maybe<ApiKey>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query);
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
    }
}