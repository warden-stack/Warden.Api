using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Types;

namespace Warden.Services.Users.Services
{
    public interface IApiKeyService
    {
        Task<Maybe<IEnumerable<ApiKeyDto>>> BrowseAsync(string userId);
        Task<Maybe<ApiKeyDto>> GetAsync(string key);
        Task<Maybe<ApiKeyDto>> GetAsync(Guid id);
        Task CreateAsync(Guid id, string userId);
        Task DeleteAsync(string key);
    }
}