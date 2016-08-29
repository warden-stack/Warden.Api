using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Domain.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Mongo
{
    public class MongoDatabaseInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly IMongoDatabase _database;

        public MongoDatabaseInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
                return;

            RegisterConventions();
            var collections = await _database.ListCollectionsAsync();
            var exists = await collections.AnyAsync();
            if(exists)
                return;

            await CreateDatabaseAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("WardenConventions", new MongoConvention(), x => true);
            _initialized = true;
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            };
        }

        private async Task CreateDatabaseAsync()
        {
            await _database.CreateCollectionAsync<Organization>();
            await _database.CreateCollectionAsync<Core.Domain.Wardens.Warden>();
            await _database.CreateCollectionAsync<WardenConfiguration>();
            await _database.CreateCollectionAsync<User>();
        }
    }
}