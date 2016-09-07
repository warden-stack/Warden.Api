﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo
{
    public class MongoDatabaseSeeder
    {
        private readonly IMongoDatabase _database;

        public MongoDatabaseSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            await CreatePaymentPlans();
            await CreateUsers();
            await CreateUserPaymentPlans();
        }

        private async Task CreatePaymentPlans()
        {
            var paymentPlans = new List<PaymentPlan>();
            var freePlan = new PaymentPlan("Free", 0);
            freePlan.AddFeatures(Feature.Organizations(1), Feature.UsersInOrganizations(3),
                Feature.Wardens(1), Feature.WardenSpawns(1), Feature.Watchers(3),
                Feature.WardenChecks(30000), Feature.WardenChecksRetentionDays(1),
                Feature.Metrics(3));

            var premiumPlan = new PaymentPlan("Premium", 10);
            premiumPlan.AddFeatures(Feature.Organizations(3), Feature.UsersInOrganizations(10),
                Feature.Wardens(3), Feature.WardenSpawns(3), Feature.Watchers(10),
                Feature.WardenChecks(300000), Feature.WardenChecksRetentionDays(7),
                Feature.Metrics(10));

            var enterprisePlan = new PaymentPlan("Enterprise", 30);
            enterprisePlan.AddFeatures(Feature.Organizations(5), Feature.UsersInOrganizations(20),
                Feature.Wardens(5), Feature.WardenSpawns(5), Feature.Watchers(20),
                Feature.WardenChecks(3000000), Feature.WardenChecksRetentionDays(30),
                Feature.Metrics(30));

            paymentPlans.Add(freePlan);
            paymentPlans.Add(premiumPlan);
            paymentPlans.Add(enterprisePlan);
            await _database.PaymentPlans().InsertManyAsync(paymentPlans);
        }

        //TODO: Create accounts via auth0
        private async Task CreateUsers()
        {
            var users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                var user = new User($"warden-user{i}@mailinator.com");
                users.Add(user);
            }
            for (int i = 1; i <- 3; i++)
            {
                var moderator = new User($"warden-moderator{i}@mailinator.com", Role.Moderator);
                users.Add(moderator);
            }
            for (int i = 1; i <- 3; i++)
            {
                var admin = new User($"warden-admin{i}@mailinator.com", Role.Admin);
                users.Add(admin);
            }
            await _database.Users().InsertManyAsync(users);
        }

        private async Task CreateUserPaymentPlans()
        {
            var users = await _database.Users()
                .AsQueryable()
                .Where(x => x.Role == Role.User)
                .ToListAsync();

            var paymentPlans = await _database.PaymentPlans().GetAllAsync();
            var freePaymentPlan = paymentPlans.First(x => x.IsFree);

            var userPaymentPlans = new List<UserPaymentPlan>();
            foreach (var user in users)
            {
                var userPaymentPlan = new UserPaymentPlan(user, freePaymentPlan);
                userPaymentPlan.AddMonthlySubscription(DateTime.UtcNow, userPaymentPlan.Features);
                user.SetPaymentPlan(userPaymentPlan);
                userPaymentPlans.Add(userPaymentPlan);
            }

            await _database.UserPaymentPlans().InsertManyAsync(userPaymentPlans);

            foreach (var user in users)
            {
                await _database.Users().ReplaceOneAsync(x => x.Id == user.Id, user);
            }
        }
    }
}