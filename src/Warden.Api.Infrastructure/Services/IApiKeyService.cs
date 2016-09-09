using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Services
{
    public interface IApiKeyService
    {
        Task<ApiKey> GetAsync(string key);
        Task CreateAsync(Guid userId);
        Task DeleteAsync(string key);
    }

    public class ApiKeyService : IApiKeyService
    {
        private readonly int RetryTimes = 5;
        private readonly IApiKeyRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly FeatureSettings _featureSettings;

        public ApiKeyService(IApiKeyRepository repository,
            IEncrypter encrypter, 
            FeatureSettings featureSettings)
        {
            _repository = repository;
            _encrypter = encrypter;
            _featureSettings = featureSettings;
        }

        public async Task<ApiKey> GetAsync(string key)
        {
            var apiKeyValue = await _repository.GetAsync(key);
            if (apiKeyValue.HasNoValue)
                throw new ServiceException($"Desired API key does not exist! Key: {key}.");

            return apiKeyValue.Value;
        }

        public async Task CreateAsync(Guid userId)
        {
            var userApiKeys = await _repository.BrowseByUserId(userId);
            var apiKeysCount = userApiKeys.Count();
            if (apiKeysCount >= _featureSettings.MaxApiKeys)
            {
                throw new ServiceException($"Limit of {_featureSettings.MaxApiKeys} " +
                                           "API keys has been reached.");
            }

            var isValid = false;
            var currentTry = 0;
            var key = string.Empty;
            while (currentTry < RetryTimes)
            {
                key = _encrypter.GetRandomSecureKey();
                isValid = (await _repository.GetAsync(key)).HasNoValue;
                if (isValid)
                    break;

                currentTry++;
            }

            if (!isValid)
                throw new ServiceException("Could not create an API key, please try again.");

            await _repository.CreateAsync(userId, key);
        }

        public async Task DeleteAsync(string key)
        {
            var apiKey = await GetAsync(key);
            await _repository.DeleteAsync(apiKey);
        }
    }
}