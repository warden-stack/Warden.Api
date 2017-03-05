using System;
using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Api.Storage
{
    public interface IUserStorage
    {
        Task<Maybe<AvailableResource>> IsNameAvailableAsync(string name);
        Task<Maybe<User>> GetAsync(string userId);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<UserSession>> GetSessionAsync(Guid id);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query);
    }
}