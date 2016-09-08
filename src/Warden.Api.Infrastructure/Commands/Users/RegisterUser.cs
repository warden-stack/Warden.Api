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

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.CreateAsync(command.Email, command.ExternalId);
        }
    }
}