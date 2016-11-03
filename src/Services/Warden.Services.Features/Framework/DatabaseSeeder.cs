using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Mongo;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories.Queries;

namespace Warden.Services.Features.Framework
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
            await _database.CreateCollectionAsync<PaymentPlan>();
            await _database.CreateCollectionAsync<UserPaymentPlan>();
            await CreatePaymentPlansAsync();
        }
        private async Task CreatePaymentPlansAsync()
        {
            var paymentPlans = new List<PaymentPlan>();
            var freePlan = new PaymentPlan("Free", 0);
            freePlan.AddFeatures(Feature.Organizations(1), Feature.UsersInOrganizations(3),
                Feature.Wardens(3), Feature.WardenSpawns(1), Feature.Watchers(3),
                Feature.WardenChecks(30000), Feature.WardenChecksRetentionDays(1),
                Feature.Metrics(3), Feature.ApiKeys(2));

            var premiumPlan = new PaymentPlan("Premium", 10);
            premiumPlan.AddFeatures(Feature.Organizations(3), Feature.UsersInOrganizations(10),
                Feature.Wardens(5), Feature.WardenSpawns(3), Feature.Watchers(10),
                Feature.WardenChecks(300000), Feature.WardenChecksRetentionDays(7),
                Feature.Metrics(10), Feature.ApiKeys(5));

            var enterprisePlan = new PaymentPlan("Enterprise", 30);
            enterprisePlan.AddFeatures(Feature.Organizations(5), Feature.UsersInOrganizations(20),
                Feature.Wardens(10), Feature.WardenSpawns(5), Feature.Watchers(20),
                Feature.WardenChecks(3000000), Feature.WardenChecksRetentionDays(30),
                Feature.Metrics(30), Feature.ApiKeys(10));

            paymentPlans.Add(freePlan);
            paymentPlans.Add(premiumPlan);
            paymentPlans.Add(enterprisePlan);
            await _database.PaymentPlans().InsertManyAsync(paymentPlans);
        }
    }
}