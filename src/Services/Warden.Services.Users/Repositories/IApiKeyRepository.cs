using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Domain;

namespace Warden.Services.Users.Repositories
{
    public interface IApiKeyRepository
    {
        Task<Maybe<IEnumerable<ApiKey>>> BrowseByUserId(string userId);
        Task<Maybe<ApiKey>> GetAsync(Guid id);
        Task<Maybe<ApiKey>> GetByKeyAsync(string key);
        Task AddAsync(ApiKey apiKey);
        Task DeleteAsync(string key);
    }
}