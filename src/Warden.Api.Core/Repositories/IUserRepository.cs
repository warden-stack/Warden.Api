using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Maybe<User>> GetAsync(Guid id);
        Task<Maybe<User>> GetAsync(string externalId);
        Task<Maybe<User>> GetByEmailAsync(string email);
        Task CreateAsync(string email, string externalId);
    }
}