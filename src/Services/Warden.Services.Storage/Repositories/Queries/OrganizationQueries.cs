using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.DTO.Organizations;
using Warden.Common.Mongo;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class OrganizationQueries
    {
        public static IMongoCollection<OrganizationDto> Organizations(this IMongoDatabase database)
            => database.GetCollection<OrganizationDto>();

        public static async Task<OrganizationDto> GetAsync(this IMongoCollection<OrganizationDto> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<OrganizationDto> GetAsync(this IMongoCollection<OrganizationDto> organizations,
            string userId, string name)
        {
            if (userId.Empty() || name.Empty())
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.OwnerId == userId && x.Name == name);
        }

        public static async Task<OrganizationDto> GetByNameForOwnerAsync(
            this IMongoCollection<OrganizationDto> organizations,
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

        public static IMongoQueryable<OrganizationDto> Query(this IMongoCollection<OrganizationDto> organizations,
            BrowseOrganizations query)
        {
            var values = organizations.AsQueryable();
            if (query.UserId.Empty() == false)
                values = values.Where(x => x.Users.Any(u => u.UserId == query.UserId));
            if (query.OwnerId.Empty() == false)
                values = values.Where(x => x.OwnerId == query.OwnerId);

            return values.OrderBy(x => x.Name);
        }

        public static async Task<bool> ExistsAsync(this IMongoCollection<OrganizationDto> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return false;

            return await organizations.AsQueryable().AnyAsync(x => x.Id == id);
        }
    }
}