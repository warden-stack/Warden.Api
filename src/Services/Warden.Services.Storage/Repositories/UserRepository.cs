using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.DTO.Users;
using Warden.Services.Storage.Queries;
using Warden.Services.Storage.Repositories.Queries;

namespace Warden.Services.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<UserDto>> GetByIdAsync(string id) => await _database.Users().GetByIdAsync(id);

        public async Task<Maybe<UserDto>> GetByEmailAsync(string email) => await _database.Users().GetByEmailAsync(email);

        public async Task AddAsync(UserDto user) => await _database.Users().InsertOneAsync(user);
        public async Task UpdateAsync(UserDto user) => await _database.Users().ReplaceOneAsync(x => x.UserId == user.UserId, user);
    }
}