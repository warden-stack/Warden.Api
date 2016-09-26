using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Mongo.Queries;
using Warden.Common.Types;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class UserPaymentPlanRepository : IUserPaymentPlanRepository
    {
        private readonly IMongoDatabase _database;

        public UserPaymentPlanRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<UserPaymentPlan>> GetAsync(Guid id)
            => await _database.UserPaymentPlans().GetByIdAsync(id);

        public async Task UpdateAsync(UserPaymentPlan paymentPlan)
            => await _database.UserPaymentPlans().ReplaceOneAsync(x => x.Id == paymentPlan.Id, paymentPlan);

        public async Task AddAsync(UserPaymentPlan paymentPlan)
            => await _database.UserPaymentPlans().InsertOneAsync(paymentPlan);
    }
}