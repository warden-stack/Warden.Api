using System;
using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Api.Storage
{
    public class UserStorage : IUserStorage
    {
        private readonly IStorageClient _storageClient;

        public UserStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<AvailableResource>> IsNameAvailableAsync(string name)
            => await _storageClient.GetAsync<AvailableResource>($"usernames/{name}/available");

        public async Task<Maybe<User>> GetAsync(string userId)
            => await _storageClient.GetAsync<User>($"users/{userId}");

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await _storageClient.GetAsync<User>($"users/{name}/account");

        public async Task<Maybe<UserSession>> GetSessionAsync(Guid id)
            => await _storageClient.GetAsync<UserSession>($"user-sessions/{id}");

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query)
            => await _storageClient.GetFilteredCollectionUsingCacheAsync<User, BrowseUsers>(query, "users");
    }
}