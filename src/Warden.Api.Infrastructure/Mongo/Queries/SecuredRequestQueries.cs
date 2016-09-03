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

        public static async Task<SecuredRequest> GetByResourceIdAsync(
            this IMongoCollection<SecuredRequest> securedRequests,
            Guid resourceId)
        {
            if (resourceId.IsEmpty())
                return null;

            return await securedRequests.AsQueryable().FirstOrDefaultAsync(x => x.ResourceId == resourceId);
        }
    }
}