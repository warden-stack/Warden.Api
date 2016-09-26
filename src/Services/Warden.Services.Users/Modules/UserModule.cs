using Nancy;
using Warden.Common.DTO.Users;
using Warden.Common.Types;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class UserModule : NancyModule
    {
        private readonly IUserService _userService;

        public UserModule(IUserService userService) : base("/users")
        {
            _userService = userService;
            Get("/{id}", async args =>
            {
                var user = await _userService.GetAsync((string) args.id);
                if (user.HasValue)
                    return user.Value;

                return HttpStatusCode.NotFound;
            });
        }
    }
}