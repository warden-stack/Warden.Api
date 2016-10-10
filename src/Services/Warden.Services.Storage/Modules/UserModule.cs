using Warden.DTO.Users;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule(IUserProvider userProvider) : base("users")
        {
            Get("{id}", async args => await Fetch<GetUser, UserDto>
                (async x => await userProvider.GetAsync(x.Id)).HandleAsync());
        }
    }
}