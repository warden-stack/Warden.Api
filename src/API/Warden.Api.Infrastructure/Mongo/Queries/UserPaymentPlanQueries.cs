using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Infrastructure.Mongo.Queries
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
    }
}