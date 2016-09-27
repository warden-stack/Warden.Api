using Nancy;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Modules
{
    public class UserModule : NancyModule
    {
        private readonly IUserRepository _userRepository;

        public UserModule(IUserRepository userRepository) : base("users")
        {
            _userRepository = userRepository;

            Get("{id}", async args =>
            {
                var user = await _userRepository.GetByIdAsync((string) args.id);
                if (user.HasValue)
                    return user.Value;

                return HttpStatusCode.NotFound;
            });
        }
    }
}