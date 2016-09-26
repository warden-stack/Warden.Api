using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public class UserStorage : IUserStorage
    {
        private readonly IStorageClient _storageClient;

        public UserStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<UserDto>> GetAsync(string id)
            => await _storageClient.GetAsync<UserDto>($"users/{id}");
    }
}