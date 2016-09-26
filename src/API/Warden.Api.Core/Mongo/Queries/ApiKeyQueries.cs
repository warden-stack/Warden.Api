using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.Users;
using Warden.Common.Extensions;
using MongoDB.Driver.Linq;

namespace Warden.Api.Core.Mongo.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKey> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKey>();

        public static async Task<IEnumerable<ApiKey>> BrowseByUserIdAsync(this IMongoCollection<ApiKey> apiKeys,
            string userId)
        {
            if (userId.Empty())
                return Enumerable.Empty<ApiKey>();

            return await apiKeys.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
        }

        public static async Task<ApiKey> GetAsync(this IMongoCollection<ApiKey> apiKeys,
            string key)
        {
            if (key.Empty())
                return null;

            return await apiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Key == key);
        }

        public static async Task<ApiKey> GetAsync(this IMongoCollection<ApiKey> apiKeys, Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await apiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}