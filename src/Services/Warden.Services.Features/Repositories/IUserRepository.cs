using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Repositories
{
    public interface IUserRepository
    {
        Task<Maybe<User>> GetAsync(Guid id);
        Task<Maybe<User>> GetAsync(string userId);
        Task<Maybe<User>> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}