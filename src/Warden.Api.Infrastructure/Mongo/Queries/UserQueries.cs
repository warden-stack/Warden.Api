using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Extensions;
using System.Linq;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Infrastructure.Queries.Organizations;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class UserQueries
    {
        public static IMongoCollection<User> Users(this IMongoDatabase database)
            => database.GetCollection<User>();

        public static async Task<User> GetByIdAsync(this IMongoCollection<User> users,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}