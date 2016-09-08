using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.DTO.Users;

namespace Warden.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(string externalId)
        {
            var user = await _userRepository.GetAsync(externalId);
            if (user.HasNoValue)
                throw new ServiceException($"Desired user does not exist, externalId: {externalId}");

            var result = _mapper.Map<UserDto>(user.Value);

            return result;
        }

        public async Task CreateAsync(string email, string externalId)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasValue)
                throw new ServiceException($"User with e-mail: {email} already exists");

            await _userRepository.CreateAsync(email, externalId);
        }
    }
}