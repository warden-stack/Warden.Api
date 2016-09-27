using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;

namespace Warden.Services.Users.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IMongoDatabase _database;

        public ApiKeyRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ApiKey>> BrowseByUserId(string userId)
            => await _database.ApiKeys().BrowseByUserIdAsync(userId);

        public async Task<Maybe<ApiKey>> GetAsync(Guid id)
            => await _database.ApiKeys().GetAsync(id);

        public async Task<Maybe<ApiKey>> GetByKeyAsync(string key)
            => await _database.ApiKeys().GetAsync(key);

        public async Task AddAsync(ApiKey apiKey)
            => await _database.ApiKeys().InsertOneAsync(apiKey);

        public async Task DeleteAsync(string key)
            => await _database.ApiKeys().DeleteOneAsync(x => x.Key == key);
    }
}