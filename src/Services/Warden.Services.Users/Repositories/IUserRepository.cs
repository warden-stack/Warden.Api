using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Domain;

namespace Warden.Services.Users.Repositories
{
    public interface IUserRepository
    {
        Task<Maybe<User>> GetAsync(Guid id);
        Task<Maybe<User>> GetAsync(string externalId);
        Task<Maybe<User>> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}