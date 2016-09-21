using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Domain.Wardens;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class WardenConfigurationQueries
    {
        public static IMongoCollection<WardenConfiguration> WardenConfigurations(this IMongoDatabase database)
            => database.GetCollection<WardenConfiguration>();

        public static async Task<WardenConfiguration> GetByIdAsync(this IMongoCollection<WardenConfiguration> configurations,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await configurations.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}