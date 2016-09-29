using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Domain;

namespace Warden.Services.Users.Services
{
    public interface IApiKeyService
    {
        Task<Maybe<IEnumerable<ApiKey>>> BrowseAsync(string userId);
        Task<Maybe<ApiKey>> GetAsync(string key);
        Task<Maybe<ApiKey>> GetAsync(Guid id);
        Task CreateAsync(Guid id, string userId);
        Task DeleteAsync(string key);
    }
}