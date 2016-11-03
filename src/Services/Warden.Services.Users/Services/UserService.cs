using System.Threading.Tasks;
using Warden.Common.Domain;
using Warden.Common.Types;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Repositories;

namespace Warden.Services.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Maybe<User>> GetAsync(string id)
            => await _userRepository.GetAsync(id);

        public async Task CreateAsync(string userId, string email, string role, bool activate = true)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasValue)
                throw new ServiceException($"User with id: {userId} already exists");

            user = await _userRepository.GetByEmailAsync(email);
            if (user.HasValue)
                throw new ServiceException($"User with e-mail: {email} already exists");

            user = new User(userId, email, role);
            if (activate)
                user.Value.Activate();

            await _userRepository.AddAsync(user.Value);
        }
    }
}