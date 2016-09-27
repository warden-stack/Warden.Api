using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.DTO.Users;
using Warden.Services.Mongo;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Framework
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IMongoDatabase _database;
        private readonly IApiKeyService _apiKeyService;

        public DatabaseSeeder(IMongoDatabase database, IApiKeyService apiKeyService)
        {
            _database = database;
            _apiKeyService = apiKeyService;
        }

        public async Task SeedAsync()
        {
            await _database.CreateCollectionAsync<ApiKey>();
            await _database.CreateCollectionAsync<User>();
            var users = new List<User>();
            for (var i = 1; i <= 10; i++)
            {
                var user = new User($"warden-user{i}@mailinator.com", Guid.NewGuid().ToString("N"));
                user.Activate();
                if (i == 1)
                    user.SetUserId("57d068eaf78ad35973d0a747");

                users.Add(user);
            }
            for (var i = 1; i < -3; i++)
            {
                var moderator = new User($"warden-moderator{i}@mailinator.com", Guid.NewGuid().ToString("N"),
                    Role.Moderator);
                moderator.Activate();
                users.Add(moderator);
            }
            for (var i = 1; i < -3; i++)
            {
                var admin = new User($"warden-admin{i}@mailinator.com", Guid.NewGuid().ToString("N"), Role.Admin);
                admin.Activate();
                users.Add(admin);
            }
            await _database.Users().InsertManyAsync(users);
            foreach (var user in users)
            {
                await _apiKeyService.CreateAsync(Guid.NewGuid(), user.UserId);
            }
        }
    }
}