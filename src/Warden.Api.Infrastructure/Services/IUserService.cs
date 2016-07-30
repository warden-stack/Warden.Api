using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Users;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(Guid id);
    }
}