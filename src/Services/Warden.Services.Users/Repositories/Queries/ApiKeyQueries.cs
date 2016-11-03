using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Users.Repositories.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKey> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKey>();

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

        public static IMongoQueryable<ApiKey> Query(this IMongoCollection<ApiKey> apiKeys,
            BrowseApiKeys query)
        {
            var values = apiKeys.AsQueryable();
            if (!query.UserId.Empty())
                values = values.Where(x => x.UserId == query.UserId);

            return values;
        }
    }
}