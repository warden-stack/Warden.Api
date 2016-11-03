using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Domain.Security;
using Warden.Api.Mongo.Queries;
using Warden.Api.Repositories;
using Warden.Common.Types;

namespace Warden.Api.Mongo.Repositories
{
    public class SecuredRequestRepository : ISecuredRequestRepository
    {
        private readonly IMongoDatabase _database;

        public SecuredRequestRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<SecuredRequest>> GetAsync(Guid id)
            => await _database.SecuredRequests().GetByIdAsync(id);

        public async Task<Maybe<SecuredRequest>> GetByResourceTypeAndIdAndTokenAsync(ResourceType resourceType,
                Guid resourceId, string token)
            => await _database.SecuredRequests().GetByResourceTypeAndIdAndTokenAsync(resourceType, resourceId, token);

        public async Task AddAsync(SecuredRequest securedRequest)
            => await _database.SecuredRequests().InsertOneAsync(securedRequest);

        public async Task UpdateAsync(SecuredRequest securedRequest)
            => await _database.SecuredRequests().ReplaceOneAsync(x => x.Id == securedRequest.Id, securedRequest);
    }
}