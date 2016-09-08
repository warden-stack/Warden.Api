using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKey> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKey>();

        public static async Task<IEnumerable<ApiKey>> BrowseByUserIdAsync(this IMongoCollection<ApiKey> apiKeys,
            Guid userId)
        {
            if (userId.IsEmpty())
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
    }
}