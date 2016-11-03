using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Queries;
using Warden.Services.Organizations.Repositories.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Organizations.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoDatabase _database;

        public OrganizationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<PagedResult<Organization>>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10)
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

        public async Task<Maybe<Organization>> GetAsync(Guid id)
            => await _database.Organizations().GetByIdAsync(id);

        public async Task<Maybe<Organization>> GetAsync(string name, string ownerId) =>
            await _database.Organizations().GetByNameForOwnerAsync(name, ownerId);
        
        public async Task UpdateAsync(Organization organization)
            => await _database.Organizations().ReplaceOneAsync(x => x.Id == organization.Id, organization);

        public async Task AddAsync(Organization organization)
            => await _database.Organizations().InsertOneAsync(organization);

        public async Task DeleteAsync(Organization organization)
            => await _database.Organizations().DeleteOneAsync(x => x.Id == organization.Id && x.Name == organization.Name);
    }
}