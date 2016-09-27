using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Extensions;
using Warden.Services.Mongo;

namespace Warden.Services.Storage.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKeyDto> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKeyDto>();

        public static async Task<IEnumerable<ApiKeyDto>> BrowseAsync(this IMongoCollection<ApiKeyDto> apiKeys,
            string userId)
        {
            if (userId.Empty())
                return Enumerable.Empty<ApiKeyDto>();

            return await apiKeys.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
        }
    }
}