using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class WardenRepository : IWardenRepository
    {
        private readonly IMongoDatabase _database;

        public WardenRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<Core.Domain.Wardens.Warden>> GetAsync(Guid id) 
            => await _database.Wardens().GetByIdAsync(id);

        public async Task AddAsync(Core.Domain.Wardens.Warden warden)
            => await _database.Wardens().InsertOneAsync(warden);

        public async Task<bool> ExistsAsync(Guid id)
            => await _database.Wardens().ExistsAsync(id);

        public async Task<bool> ExistsAsync(string internalId)
            => await _database.Wardens().ExistsAsync(internalId);
    }
}