using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IMongoDatabase _database;

        public ApiKeyRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<ApiKeyDto>> GetAsync(string key)
            => await _database.ApiKeys().GetAsync(key);

        public async Task<Maybe<IEnumerable<ApiKeyDto>>> BrowseAsync(string userId)
            => await _database.ApiKeys().BrowseAsync(userId);

        public async Task AddManyAsync(IEnumerable<ApiKeyDto> apiKeys)
            => await _database.ApiKeys().InsertManyAsync(apiKeys);

        public async Task AddAsync(ApiKeyDto apiKey)
            => await _database.ApiKeys().InsertOneAsync(apiKey);

        public async Task DeleteAsync(string key)
            => await _database.ApiKeys().DeleteOneAsync(x => x.Key == key);
    }
}