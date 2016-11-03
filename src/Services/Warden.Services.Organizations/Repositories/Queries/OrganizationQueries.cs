using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Organizations.Repositories.Queries
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
            string name, string ownerId)
        {
            if (name.Empty() || ownerId.Empty())
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
            if (query.UserId.Empty())
                values = values.Where(x => x.Users.Any(u => u.UserId == query.UserId));
            if (query.OwnerId.Empty() == false)
                values = values.Where(x => x.OwnerId == query.OwnerId);

            return values.OrderBy(x => x.Name);
        }

        public static async Task<bool> ExistsAsync(this IMongoCollection<Organization> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return false;

            return await organizations.AsQueryable().AnyAsync(x => x.Id == id);
        }
    }
}