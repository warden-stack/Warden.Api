using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Services.Mongo;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;

namespace Warden.Services.Users.Framework
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IMongoDatabase _database;

        public DatabaseSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            await _database.CreateCollectionAsync<User>();
            var users = new List<User>();
            for (var i = 1; i <= 10; i++)
            {
                var user = new User($"warden-user{i}@mailinator.com");
                user.Activate();
                if (i == 1)
                    user.SetExternalId("57d068eaf78ad35973d0a747");

                users.Add(user);
            }
            for (var i = 1; i < -3; i++)
            {
                var moderator = new User($"warden-moderator{i}@mailinator.com", Role.Moderator);
                moderator.Activate();
                users.Add(moderator);
            }
            for (var i = 1; i < -3; i++)
            {
                var admin = new User($"warden-admin{i}@mailinator.com", Role.Admin);
                admin.Activate();
                users.Add(admin);
            }
            await _database.Users().InsertManyAsync(users);
        }
    }
}