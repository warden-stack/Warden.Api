using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule(IUserService userService) : base("users")
        {
            Get("{id}", async args => await Fetch<GetUser, User>
                (async x => await userService.GetAsync(x.Id)).HandleAsync());
        }
    }
}