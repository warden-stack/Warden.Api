using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class WardenQueries
    {
        public static IMongoCollection<Core.Domain.Wardens.Warden> Wardens(this IMongoDatabase database)
            => database.GetCollection<Core.Domain.Wardens.Warden>();

        public static async Task<Core.Domain.Wardens.Warden> GetByIdAsync(
            this IMongoCollection<Core.Domain.Wardens.Warden> wardens,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await wardens.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<bool> ExistsAsync(this IMongoCollection<Core.Domain.Wardens.Warden> wardens,
            Guid id)
        {
            if (id.IsEmpty())
                return false;

            return await wardens.AsQueryable().AnyAsync(x => x.Id == id);
        }

        public static async Task<bool> ExistsAsync(this IMongoCollection<Core.Domain.Wardens.Warden> wardens,
            string internalId)
        {
            if (internalId.Empty())
                return false;

            return await wardens.AsQueryable().AnyAsync(x => x.InternalId == internalId);
        }
    }
}