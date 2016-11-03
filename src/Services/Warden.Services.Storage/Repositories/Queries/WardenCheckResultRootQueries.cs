using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.DTO.Wardens;
using Warden.Common.Mongo;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class WardenCheckResultRootQueries
    {
        public static IMongoCollection<WardenCheckResultRootDto> WardenCheckResultRoots(this IMongoDatabase database)
            => database.GetCollection<WardenCheckResultRootDto>();

        public static IMongoQueryable<WardenCheckResultRootDto> Query(this IMongoCollection<WardenCheckResultRootDto> checkResults,
            BrowseWardenCheckResults query)
        {
            var values = checkResults.AsQueryable();
            if (query.OrganizationId != Guid.Empty)
                values = values.Where(x => x.OrganizationId == query.OrganizationId);
            if (query.WardenId != Guid.Empty)
                values = values.Where(x => x.WardenId == query.WardenId);

            return values.OrderByDescending(x => x.CreatedAt);
        }
    }
}