﻿using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.DTO.Users;
using Warden.Common.Extensions;
using Warden.Services.Mongo;

namespace Warden.Services.Storage.Queries
{
    public static class UserQueries
    {
        public static IMongoCollection<UserDto> Users(this IMongoDatabase database)
            => database.GetCollection<UserDto>();

        public static async Task<UserDto> GetByIdAsync(this IMongoCollection<UserDto> users, string id)
        {
            if (id.Empty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.UserId == id);
        }

        public static async Task<UserDto> GetByEmailAsync(this IMongoCollection<UserDto> users, string email)
        {
            if (email.Empty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}