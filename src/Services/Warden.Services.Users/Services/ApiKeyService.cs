using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Types;
using Warden.Services.Domain;
using Warden.Services.Encryption;
using Warden.Services.Users.Domain;
using Warden.Services.Users.Repositories;
using Warden.Services.Users.Settings;

namespace Warden.Services.Users.Services
{
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

        public async Task<Maybe<IEnumerable<ApiKeyDto>>> BrowseAsync(string userId)
        {
            var apiKeys = await _repository.BrowseByUserId(userId);
            var dtos = apiKeys.Select(x => new ApiKeyDto
            {
                UserId = x.UserId,
                Key = x.Key
            }).ToList();

            return dtos;
        }

        public async Task<Maybe<ApiKeyDto>> GetAsync(string key)
        {
            var apiKey = await _repository.GetByKeyAsync(key);
            if (apiKey.HasNoValue)
                return new Maybe<ApiKeyDto>();

            var dto = new ApiKeyDto
            {
                Key = apiKey.Value.Key,
                UserId = apiKey.Value.UserId
            };

            return dto;
        }

        public async Task<Maybe<ApiKeyDto>> GetAsync(Guid id)
        {
            var apiKey = await _repository.GetAsync(id);
            if (apiKey.HasNoValue)
                return new Maybe<ApiKeyDto>();

            var dto = new ApiKeyDto
            {
                Key = apiKey.Value.Key,
                UserId = apiKey.Value.UserId
            };

            return dto;
        }

        public async Task CreateAsync(Guid id, string userId)
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