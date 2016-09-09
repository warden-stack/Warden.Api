using System.Threading.Tasks;
using Warden.Api.Core.Events.Users;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Events.Handlers.Users
{
    public class NewUserSignedInHandler : IEventHandler<NewUserSignedIn>
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly IApiKeyService _apiKeyService;

        public NewUserSignedInHandler(IUserService userService,
            IOrganizationService organizationService,
            IApiKeyService apiKeyService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _apiKeyService = apiKeyService;
        }

        public async Task HandleAsync(NewUserSignedIn @event)
        {
            await _userService.CreateAsync(@event.Email, @event.ExternalId);
            var user = await _userService.GetByEmailAsync(@event.Email);
            await _organizationService.CreateDefaultAsync(user.Id);
            await _apiKeyService.CreateAsync(user.Id);
        }
    }
}