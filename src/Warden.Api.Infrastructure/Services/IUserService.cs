using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Users;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string externalId);
        Task CreateAsync(string email, string externalId);
    }
}