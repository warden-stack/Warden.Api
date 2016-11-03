using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;
using Warden.Services.Storage.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKeyDto> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKeyDto>();

        public static async Task<Maybe<ApiKeyDto>> GetAsync(this IMongoCollection<ApiKeyDto> apiKeys,
            string key)
        {
            if (key.Empty())
                return new Maybe<ApiKeyDto>();

            return await apiKeys.FirstOrDefaultAsync(x => x.Key == key);
        }

        public static IMongoQueryable<ApiKeyDto> Query(this IMongoCollection<ApiKeyDto> apiKeys,
            BrowseApiKeys query)
        {
            var values = apiKeys.AsQueryable();
            if (!query.UserId.Empty())
                values = values.Where(x => x.UserId == query.UserId);

            return values;
        }
    }
}