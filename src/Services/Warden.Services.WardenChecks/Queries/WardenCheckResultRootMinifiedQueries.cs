using MongoDB.Driver;
using Warden.Services.WardenChecks.Domain.Minified;
using Warden.Common.Mongo;

namespace Warden.Services.WardenChecks.Queries
{
    public static class WardenCheckResultRootMinifiedQueries
    {
        public static IMongoCollection<WardenCheckResultRootMinified> WardenCheckResultRootMinifieds(
                this IMongoDatabase database)
            => database.GetCollection<WardenCheckResultRootMinified>();
    }
}