using Nancy;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Modules
{
    public class UserPaymentPlanModule : NancyModule
    {
        private readonly IUserPaymentPlanService _userPaymentPlanService;

        public UserPaymentPlanModule(IUserPaymentPlanService userPaymentPlanService) : base("/users/{userId}/plans")
        {
            _userPaymentPlanService = userPaymentPlanService;
            Get("/current", async args =>
            {
                var plan = await _userPaymentPlanService.GetCurrentPlanAsync((string) args.userId);
                if (plan.HasValue)
                    return plan.Value;

                return HttpStatusCode.NotFound;
            });
        }
    }
}