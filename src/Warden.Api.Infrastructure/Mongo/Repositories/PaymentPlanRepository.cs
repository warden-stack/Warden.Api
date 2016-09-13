using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.Mongo.Queries;

namespace Warden.Api.Infrastructure.Mongo.Repositories
{
    public class PaymentPlanRepository : IPaymentPlanRepository
    {
        private readonly IMongoDatabase _database;

        public PaymentPlanRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PaymentPlan> GetDefaultAsync()
            => await _database.PaymentPlans().GetDefaultAsync();

        public async Task<Maybe<PaymentPlan>> GetAsync(Guid id)
            => await _database.PaymentPlans().GetByIdAsync(id);

        public async Task<IEnumerable<PaymentPlan>> GetAllAsync()
            => await _database.PaymentPlans().GetAllAsync();
    }
}