using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Extensions;
using System.Linq;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Infrastructure.Queries.Organizations;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class OrganizationQueries
    {
        public static IMongoCollection<Organization> Organizations(this IMongoDatabase database)
            => database.GetCollection<Organization>();

        public static async Task<Organization> GetByIdAsync(this IMongoCollection<Organization> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<Organization> GetByNameForOwnerAsync(this IMongoCollection<Organization> organizations,
            string name, Guid ownerId)
        {
            if (name.Empty() || ownerId.IsEmpty())
                return null;

            var fixedName = name.TrimToLower();

            return await organizations
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == fixedName
                                          && x.OwnerId == ownerId);
        }

        public static IMongoQueryable<Organization> Query(this IMongoCollection<Organization> organizations,
            BrowseOrganizations query)
        {
            var values = organizations.AsQueryable();
            if (query.UserId.IsEmpty() == false)
                values = values.Where(x => x.Users.Any(u => u.Id == query.UserId));
            if (query.OwnerId.IsEmpty() == false)
                values = values.Where(x => x.OwnerId == query.OwnerId);

            return values.OrderBy(x => x.Name);
        }
    }
}