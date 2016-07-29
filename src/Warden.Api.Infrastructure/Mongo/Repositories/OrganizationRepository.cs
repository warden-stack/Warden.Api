using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Queries;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Mongo.Queries;

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

        public async Task<PagedResult<Organization>> BrowseAsync(BrowseOrganizations query)
        {
            if (query == null)
                return PagedResult<Organization>.Empty;

            return await _database.Organizations()
                .Query(query)
                .PaginateAsync(query);
        }

        public Task AddAsync(Organization organization)
        {
            throw new NotImplementedException();
        }
    }
}