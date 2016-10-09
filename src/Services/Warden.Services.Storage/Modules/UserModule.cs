using Nancy;
using Warden.Services.Storage.Providers;

namespace Warden.Services.Storage.Modules
{
    public class UserModule : ModuleBase
    {
        private readonly IUserProvider _userProvider;

        public UserModule(IUserProvider userProvider) : base("users")
        {
            _userProvider = userProvider;

            Get("/{id}", async args =>
            {
                var user = await _userProvider.GetAsync((string) args.id);
                if (user.HasValue)
                    return user.Value;

                return HttpStatusCode.NotFound;
            });
        }
    }
}