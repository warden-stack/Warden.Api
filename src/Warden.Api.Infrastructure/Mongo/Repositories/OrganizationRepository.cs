using System;
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

        public IQueryable<Organization> Browse(BrowseOrganizations query)
        {
            if (query == null)
                return Enumerable.Empty<Organization>().AsQueryable();

            var organizations = _database.Organizations()
                .Query(query)
                .OrderBy(x => x.Name);

            return organizations;
        }

        public Task AddAsync(Organization organization)
        {
            throw new NotImplementedException();
        }
    }
}