using System.Threading.Tasks;
using Warden.Api.Core.Events.Users;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Events.Handlers.Users
{
    public class NewUserSignedInHandler : IEventHandler<NewUserSignedIn>
    {
        private readonly IUserService _userService;
        private readonly IUserPaymentPlanService _userPaymentPlanService;
        private readonly IOrganizationService _organizationService;
        private readonly IApiKeyService _apiKeyService;

        public NewUserSignedInHandler(IUserService userService,
            IUserPaymentPlanService userPaymentPlanService,
            IOrganizationService organizationService,
            IApiKeyService apiKeyService)
        {
            _userService = userService;
            _userPaymentPlanService = userPaymentPlanService;
            _organizationService = organizationService;
            _apiKeyService = apiKeyService;
        }

        public async Task HandleAsync(NewUserSignedIn @event)
        {
            await _userService.CreateAsync(@event.Email, @event.ExternalId);
            var user = await _userService.GetByEmailAsync(@event.Email);
            await _userPaymentPlanService.CreateDefaultAsync(user.Id);
            await _organizationService.CreateDefaultAsync(user.Id);
            await _apiKeyService.CreateAsync(user.Id);
        }
    }
}