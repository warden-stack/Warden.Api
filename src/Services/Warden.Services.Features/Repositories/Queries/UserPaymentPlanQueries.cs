using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Services.Features.Domain;
using Warden.Common.Mongo;

namespace Warden.Services.Features.Repositories.Queries
{
    public static class UserPaymentPlanQueries
    {
        public static IMongoCollection<UserPaymentPlan> UserPaymentPlans(this IMongoDatabase database)
            => database.GetCollection<UserPaymentPlan>();

        public static async Task<UserPaymentPlan> GetByIdAsync(this IMongoCollection<UserPaymentPlan> paymentPlans,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await paymentPlans.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<WardenCheckUsageInfo> GetWardenCheckUsageInfoAsync(
            this IMongoCollection<UserPaymentPlan> paymentPlans,
            string userId)
        {
            if (userId.Empty())
                return null;

            //TODO: Check by date.
            var paymentPlan = await paymentPlans.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
            var wardenChecksFeature = paymentPlan.MonthlySubscriptions.First()
                .FeatureUsages.First(x => x.Feature == FeatureType.AddWardenCheck);

            return new WardenCheckUsageInfo
            {
                UserId = userId,
                Limit = wardenChecksFeature.Limit,
                Usage = wardenChecksFeature.Usage
            };
        }
    }
}