using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Users;

namespace Warden.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public async Task<User> GetAsync(Guid id)
        {
            return new User("test@wp.pl");
        }
    }
}