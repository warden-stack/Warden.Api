using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class SecuredRequestQueries
    {
        public static IMongoCollection<SecuredRequest> SecuredRequests(this IMongoDatabase database)
            => database.GetCollection<SecuredRequest>();

        public static async Task<SecuredRequest> GetByIdAsync(this IMongoCollection<SecuredRequest> securedRequests,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await securedRequests.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<SecuredRequest> GetByResourceTypeAndIdAndTokenAsync(
            this IMongoCollection<SecuredRequest> securedRequests,
            ResourceType resourceType,
            Guid resourceId,
            string token)
        {
            if (resourceId.IsEmpty())
                return null;

            return await securedRequests.AsQueryable().FirstOrDefaultAsync(x => x.ResourceType == resourceType &&
                                                                                x.ResourceId == resourceId &&
                                                                                x.Token == token);
        }
    }
}