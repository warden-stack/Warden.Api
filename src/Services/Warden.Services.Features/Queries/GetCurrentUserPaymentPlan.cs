using Warden.Common.Queries;

namespace Warden.Services.Features.Queries
{
    public class GetCurrentUserPaymentPlan : IAuthenticatedQuery
    {
        public string UserId { get; set; }
    }
}