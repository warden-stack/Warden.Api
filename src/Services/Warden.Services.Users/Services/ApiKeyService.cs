using System;
using System.Threading.Tasks;
using Warden.Common.Domain;
using Warden.Common.Encryption;
using Warden.Common.Types;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Services.Users.Repositories;

namespace Warden.Services.Users.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly int RetryTimes = 5;
        private readonly IApiKeyRepository _repository;
        private readonly IEncrypter _encrypter;

        public ApiKeyService(IApiKeyRepository repository,
            IEncrypter encrypter)
        {
            _repository = repository;
            _encrypter = encrypter;
        }

        public async Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query)
            => await _repository.BrowseAsync(query);

        public async Task<Maybe<ApiKey>> GetAsync(string key)
            => await _repository.GetByKeyAsync(key);

        public async Task<Maybe<ApiKey>> GetAsync(Guid id) 
            => await _repository.GetAsync(id);

        public async Task CreateAsync(Guid id, string userId)
        {
            var isValid = false;
            var currentTry = 0;
            var key = string.Empty;
            while (currentTry < RetryTimes)
            {
                key = _encrypter.GetRandomSecureKey();
                isValid = (await _repository.GetByKeyAsync(key)).HasNoValue;
                if (isValid)
                    break;

                currentTry++;
            }

            if (!isValid)
                throw new ServiceException("Could not create an API key, please try again.");

            var apiKey = new ApiKey(id, key, userId);
            await _repository.AddAsync(apiKey);
        }

        public async Task DeleteAsync(string key)
        {
            var apiKey = await _repository.GetByKeyAsync(key);
            if (apiKey.HasNoValue)
                throw new ServiceException($"Desired API key does not exist! Key: {key}.");

            await _repository.DeleteAsync(apiKey.Value.Key);
        }
    }
}