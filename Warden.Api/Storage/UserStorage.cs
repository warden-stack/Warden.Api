using System;
using System.Threading.Tasks;
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

        public async Task<Maybe<UserDto>> GetAsync(string userId)
            => await _storageClient.GetUsingCacheAsync<UserDto>($"users/{userId}");

        public async Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id)
            => await _storageClient.GetAsync<UserSessionDto>($"user-sessions/{id}");
    }
}