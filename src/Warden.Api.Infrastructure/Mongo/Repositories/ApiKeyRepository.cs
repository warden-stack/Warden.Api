using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IMongoDatabase _database;

        public ApiKeyRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ApiKey>> BrowseByUserId(Guid userId)
            => await _database.ApiKeys().BrowseByUserIdAsync(userId);

        public async Task<Maybe<ApiKey>> GetAsync(Guid id)
            => await _database.ApiKeys().GetAsync(id);

        public async Task<Maybe<ApiKey>> GetByKeyAsync(string key)
            => await _database.ApiKeys().GetAsync(key);

        public async Task AddAsync(ApiKey apiKey)
            => await _database.ApiKeys().InsertOneAsync(apiKey);

        public async Task DeleteAsync(ApiKey key)
            => await _database.ApiKeys().DeleteOneAsync(x => x.UserId == key.UserId && x.Key == key.Key);
    }
}