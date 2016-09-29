using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Users;
using Warden.Services.Storage.Mappers;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.Settings;

namespace Warden.Services.Storage.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IProviderClient _providerClient;
        private readonly ProviderSettings _providerSettings;
        private readonly UserMapper _mapper;

        public UserProvider(IUserRepository userRepository,
            IProviderClient providerClient,
            ProviderSettings providerSettings,
            UserMapper mapper)
        {
            _userRepository = userRepository;
            _providerClient = providerClient;
            _providerSettings = providerSettings;
            _mapper = mapper;
        }

        public async Task<Maybe<UserDto>> GetAsync(string userId) =>
            await _providerClient.GetUsingStorageAsync(_providerSettings.UsersApiUrl,
                $"/users/{userId}", async () => await _userRepository.GetByIdAsync(userId),
                async user => await _userRepository.AddAsync(user), _mapper);
    }
}