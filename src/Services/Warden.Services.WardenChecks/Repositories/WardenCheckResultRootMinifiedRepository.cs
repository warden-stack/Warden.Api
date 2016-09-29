using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Services.WardenChecks.Domain.Minified;
using Warden.Services.WardenChecks.Queries;

namespace Warden.Services.WardenChecks.Repositories
{
    public class WardenCheckResultRootMinifiedRepository : IWardenCheckResultRootMinifiedRepository
    {
        private readonly IMongoDatabase _database;

        public WardenCheckResultRootMinifiedRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(WardenCheckResultRootMinified checkResult)
            => await _database.WardenCheckResultRootMinifieds().InsertOneAsync(checkResult);
    }
}