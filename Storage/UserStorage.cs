using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Api.Storage
{
    public class UserStorage : IUserStorage
    {
        private readonly IStorageClient _storageClient;

        public UserStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<UserDto>> GetAsync(string id)
            => await _storageClient.GetUsingCacheAsync<UserDto>($"users/{id}");
    }
}