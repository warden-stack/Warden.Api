using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;
using Warden.Services.Domain;
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

        public async Task<Maybe<UserDto>> GetAsync(string id)
        {
            var user = await _userRepository.GetAsync(FixId(id));
            if (user.HasNoValue)
                return new Maybe<UserDto>();

            return new UserDto
            {
                UserId = user.Value.UserId,
                Email = user.Value.Email
            };
        }

        public async Task CreateAsync(string email, string id, bool activate = true)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasValue)
                throw new ServiceException($"User with e-mail: {email} already exists");

            user = new User(email, FixId(id));
            if (activate)
                user.Value.Activate();

            await _userRepository.AddAsync(user.Value);
        }

        private static string FixId(string id) => id.Replace("auth0|", string.Empty);
    }
}