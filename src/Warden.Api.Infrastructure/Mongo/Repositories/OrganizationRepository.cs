using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Mongo.Queries;
using Warden.Api.Infrastructure.Queries.Organizations;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoDatabase _database;

        public OrganizationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Organization> GetAsync(Guid organizationId)
        {
            var organization = await _database.Organizations().GetByIdAsync(organizationId);

            return organization;
        }

        public async Task<Organization> GetAsync(string name, Guid ownerId)
        {
            var organization = await _database.Organizations().GetByNameForOwnerAsync(name, ownerId);

            return organization;
        }

        public async Task<PagedResult<Organization>> BrowseAsync(Guid userId, Guid ownerId,
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

        public async Task AddAsync(Organization organization)
        {
            await _database.Organizations().InsertOneAsync(organization);
        }
    }
}