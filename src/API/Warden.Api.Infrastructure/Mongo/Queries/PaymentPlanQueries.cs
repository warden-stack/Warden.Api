using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Common.Extensions;

namespace Warden.Api.Infrastructure.Mongo.Queries
{
    public static class PaymentPlanQueries
    {
        public static IMongoCollection<PaymentPlan> PaymentPlans(this IMongoDatabase database)
            => database.GetCollection<PaymentPlan>();

        public static async Task<PaymentPlan> GetDefaultAsync(this IMongoCollection<PaymentPlan> paymentPlans)
            => await paymentPlans.AsQueryable().FirstOrDefaultAsync(x => x.MonthlyPrice == 0);

        public static async Task<PaymentPlan> GetByIdAsync(this IMongoCollection<PaymentPlan> paymentPlans,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await paymentPlans.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}