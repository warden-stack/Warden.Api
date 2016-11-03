using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.DTO.Wardens;
using Warden.Services.Storage.Queries;
using Warden.Services.Storage.Repositories.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Storage.Repositories
{
    public class WardenCheckResultRootRepository : IWardenCheckResultRootRepository
    {
        private readonly IMongoDatabase _database;

        public WardenCheckResultRootRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<PagedResult<WardenCheckResultRootDto>>> BrowseAsync(Guid organizationId,
            Guid wardenId, int page = 1, int results = 10)
        {
            var query = new BrowseWardenCheckResults
            {
                OrganizationId = organizationId,
                WardenId = wardenId,
                Page = page,
                Results = results
            };

            return await _database.WardenCheckResultRoots()
                .Query(query)
                .PaginateAsync(query);
        }

        public async Task AddAsync(WardenCheckResultRootDto checkResult)
            => await _database.WardenCheckResultRoots().InsertOneAsync(checkResult);

        public async Task DeleteAsync(WardenCheckResultRootDto checkResult)
            => await _database.WardenCheckResultRoots().DeleteOneAsync(x => x.Id == checkResult.Id);
    }
}