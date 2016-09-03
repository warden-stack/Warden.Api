using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class SecuredRequestRepository : ISecuredRequestRepository
    {
        private readonly IMongoDatabase _database;

        public SecuredRequestRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<SecuredRequest>> GetByResourceIdAsync(Guid resourceId)
            => await _database.SecuredRequests().GetByResourceIdAsync(resourceId);

        public async Task AddAsync(SecuredRequest securedRequest)
            => await _database.SecuredRequests().InsertOneAsync(securedRequest);

        public async Task UpdateAsync(SecuredRequest securedRequest)
            => await _database.SecuredRequests().ReplaceOneAsync(x => x.Id == securedRequest.Id, securedRequest);
    }
}