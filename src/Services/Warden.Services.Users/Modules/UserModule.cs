using Nancy;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class UserModule : ModuleBase
    {
        private readonly IUserService _userService;

        public UserModule(IUserService userService) : base("users")
        {
            _userService = userService;
            Get("{id}", async args =>
            {
                var user = await _userService.GetAsync((string) args.id);
                if (user.HasValue)
                    return user.Value;

                return HttpStatusCode.NotFound;
            });
        }
    }
}