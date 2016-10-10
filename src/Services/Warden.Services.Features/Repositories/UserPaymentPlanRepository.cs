using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories.Queries;

namespace Warden.Services.Features.Repositories
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

        public async Task<Maybe<WardenCheckUsageInfo>> GetWardenCheckUsageInfoAsync(string userId)
            => await _database.UserPaymentPlans().GetWardenCheckUsageInfoAsync(userId);

        public async Task UpdateAsync(UserPaymentPlan paymentPlan)
            => await _database.UserPaymentPlans().ReplaceOneAsync(x => x.Id == paymentPlan.Id, paymentPlan);

        public async Task AddAsync(UserPaymentPlan paymentPlan)
            => await _database.UserPaymentPlans().InsertOneAsync(paymentPlan);
    }
}