using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Users
{
    public class RegisterUser : ICommand
    {
        public string ExternalId { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly IApiKeyService _apiKeyService;

        public RegisterUserHandler(IUserService userService, 
            IOrganizationService organizationService,
            IApiKeyService apiKeyService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _apiKeyService = apiKeyService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.CreateAsync(command.Email, command.ExternalId);
            var user = await _userService.GetAsync(command.ExternalId);
            await _organizationService.CreateDefaultAsync(user.Id);
            await _apiKeyService.CreateAsync(user.Id);
        }
    }
}