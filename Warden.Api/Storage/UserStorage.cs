using System;
using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Storage
{
    public class UserStorage : IUserStorage
    {
        private readonly IStorageClient _storageClient;

        public UserStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<AvailableResourceDto>> IsNameAvailableAsync(string name)
            => await _storageClient.GetAsync<AvailableResourceDto>($"usernames/{name}/available");

        public async Task<Maybe<UserDto>> GetAsync(string userId)
            => await _storageClient.GetAsync<UserDto>($"users/{userId}");

        public async Task<Maybe<UserDto>> GetByNameAsync(string name)
            => await _storageClient.GetAsync<UserDto>($"users/{name}/account");

        public async Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id)
            => await _storageClient.GetAsync<UserSessionDto>($"user-sessions/{id}");

        public async Task<Maybe<PagedResult<UserDto>>> BrowseAsync(BrowseUsers query)
            => await _storageClient.GetFilteredCollectionUsingCacheAsync<UserDto, BrowseUsers>(query, "users");
    }
}