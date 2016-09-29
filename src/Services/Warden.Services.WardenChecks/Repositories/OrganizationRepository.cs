using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.WardenChecks.Domain;
using Warden.Services.WardenChecks.Queries;

namespace Warden.Services.WardenChecks.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoDatabase _database;

        public OrganizationRepository(IMongoDatabase database)
        {
            _database = database;
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