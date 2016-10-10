using Warden.Services.Features.Domain;
using Warden.Services.Features.Queries;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Modules
{
    public class UserPaymentPlanModule : ModuleBase
    {
        public UserPaymentPlanModule(IUserPaymentPlanService userPaymentPlanService) : base("users/{userId}/plans")
        {
            Get("current", async args => await Fetch<GetCurrentUserPaymentPlan, UserPaymentPlan>
                (async x => await userPaymentPlanService.GetCurrentPlanAsync(x.UserId)).HandleAsync());
        }
    }
}