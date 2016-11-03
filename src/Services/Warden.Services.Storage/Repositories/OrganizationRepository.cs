using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Common.Mongo;
using Warden.DTO.Organizations;
using Warden.Services.Storage.Queries;
using Warden.Services.Storage.Repositories.Queries;

namespace Warden.Services.Storage.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoDatabase _database;

        public OrganizationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(Guid id)
            => await _database.Organizations().GetAsync(id);

        public async Task<Maybe<PagedResult<OrganizationDto>>> BrowseAsync(string userId, string ownerId, int page = 1, int results = 10)
        {
            var query = new BrowseOrganizations
            {
                OwnerId = ownerId,
                UserId = userId,
                Page = page,
                Results = results
            };

            return await _database.Organizations()
                .Query(query)
                .PaginateAsync(query);
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(string userId, string name)
            => await _database.Organizations().GetAsync(userId, name);

        public async Task UpdateAsync(OrganizationDto organization)
            => await _database.Organizations().ReplaceOneAsync(x => x.Id == organization.Id, organization);

        public async Task AddAsync(OrganizationDto organization)
            => await _database.Organizations().InsertOneAsync(organization);

        public async Task DeleteAsync(OrganizationDto organization)
            => await _database.Organizations().DeleteOneAsync(x => x.Id == organization.Id && x.Name == organization.Name);
    }
}