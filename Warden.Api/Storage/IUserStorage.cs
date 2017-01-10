using System;
using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Storage
{
    public interface IUserStorage
    {
        Task<Maybe<AvailableResourceDto>> IsNameAvailableAsync(string name);
        Task<Maybe<UserDto>> GetAsync(string userId);
        Task<Maybe<UserDto>> GetByNameAsync(string name);
        Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id);
        Task<Maybe<PagedResult<UserDto>>> BrowseAsync(BrowseUsers query);
    }
}