using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.DTO.Operations;
using Warden.Services.Storage.Repositories.Queries;

namespace Warden.Services.Storage.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IMongoDatabase _database;

        public OperationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId)
            => await _database.Operations().GetByRequestIdAsync(requestId);

        public async Task AddAsync(OperationDto operation) => await _database.Operations().InsertOneAsync(operation);

        public async Task UpdateAsync(OperationDto operation)
            => await _database.Operations().ReplaceOneAsync(x => x.Id == operation.Id, operation);
    }
}