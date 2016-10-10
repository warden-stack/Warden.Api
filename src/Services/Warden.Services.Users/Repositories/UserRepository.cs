using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Services.Users.Repositories.Queries;

namespace Warden.Services.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<User>> GetAsync(string userId) => await _database.Users().GetByUserId(userId);

        public async Task<Maybe<User>> GetByEmailAsync(string email) => await _database.Users().GetByEmailAsync(email);

        public async Task AddAsync(User user) => await _database.Users().InsertOneAsync(user);
        public async Task UpdateAsync(User user) => await _database.Users().ReplaceOneAsync(x => x.UserId == user.UserId, user);
    }
}