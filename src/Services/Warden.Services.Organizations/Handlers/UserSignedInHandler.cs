using System.Linq;
using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Events;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Users;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Repositories;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class UserSignedInHandler : IEventHandler<UserSignedIn>
    {
        private readonly IBusClient _bus;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationService _organizationService;

        public UserSignedInHandler(IBusClient bus,
            IUserRepository userRepository,
            IOrganizationService organizationService)
        {
            _bus = bus;
            _userRepository = userRepository;
            _organizationService = organizationService;
        }

        public async Task HandleAsync(UserSignedIn @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if (user.HasValue)
            {
                await CreateDefaultOrganizationIfRequiredAsync(@event.UserId);

                return;
            }

            await _userRepository.AddAsync(new User(@event.Email, @event.UserId, @event.Role));
            await CreateDefaultOrganizationIfRequiredAsync(@event.UserId);
        }

        private async Task CreateDefaultOrganizationIfRequiredAsync(string userId)
        {
            var organizations = await _organizationService.BrowseAsync(userId);
            if (organizations.Items.Any())
                return;

            await _organizationService.CreateDefaultAsync(userId);
            await _bus.PublishAsync(new OrganizationCreated(userId,
                _organizationService.DefaultOrganizationName));
        }
    }
}