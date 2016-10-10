using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Queries;
using Warden.Services.Organizations.Repositories.Queries;

namespace Warden.Services.Organizations.Repositories
{
    public class WardenConfigurationRepository : IWardenConfigurationRepository
    {
        private readonly IMongoDatabase _database;

        public WardenConfigurationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<WardenConfiguration>> GetAsync(Guid id) 
            => await _database.WardenConfigurations().GetByIdAsync(id);

        public async Task AddAsync(WardenConfiguration configuration)
            => await _database.WardenConfigurations().InsertOneAsync(configuration);
    }
}