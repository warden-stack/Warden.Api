using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories.Queries;
using Warden.Common.Mongo;

namespace Warden.Services.Features.Repositories
{
    public class PaymentPlanRepository : IPaymentPlanRepository
    {
        private readonly IMongoDatabase _database;

        public PaymentPlanRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<PaymentPlan>> GetDefaultAsync()
            => await _database.PaymentPlans().GetDefaultAsync();

        public async Task<Maybe<PaymentPlan>> GetAsync(Guid id)
            => await _database.PaymentPlans().GetByIdAsync(id);

        public async Task<IEnumerable<PaymentPlan>> GetAllAsync()
            => await _database.PaymentPlans().GetAllAsync();
    }
}