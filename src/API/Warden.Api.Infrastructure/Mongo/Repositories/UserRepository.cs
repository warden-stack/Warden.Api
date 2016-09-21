using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<User>> GetAsync(Guid id) => await _database.Users().GetByIdAsync(id);

        public async Task<Maybe<User>> GetAsync(string externalId) => await _database.Users().GetByExternalIdAsync(externalId);

        public async Task<Maybe<User>> GetByEmailAsync(string email) => await _database.Users().GetByEmailAsync(email);

        public async Task AddAsync(User user) 
            => await _database.Users().InsertOneAsync(user);
    }
}