using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Repositories;
using Warden.Common.DTO.ApiKeys;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Services
{
    public interface IApiKeyService
    {
        Task<IEnumerable<ApiKeyDto>> BrowseAsync(Guid userId);
        Task<ApiKeyDto> GetAsync(string key);
        Task<ApiKeyDto> GetAsync(Guid id);
        Task CreateAsync(Guid id, Guid userId);
        Task DeleteAsync(string key);
    }

    public class ApiKeyService : IApiKeyService
    {
        private readonly int RetryTimes = 5;
        private readonly IApiKeyRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;
        private readonly FeatureSettings _featureSettings;

        public ApiKeyService(IApiKeyRepository repository,
            IEncrypter encrypter, 
            IMapper mapper,
            FeatureSettings featureSettings)
        {
            _repository = repository;
            _encrypter = encrypter;
            _mapper = mapper;
            _featureSettings = featureSettings;
        }

        public async Task<IEnumerable<ApiKeyDto>> BrowseAsync(Guid userId)
        {
            var apiKeys = await _repository.BrowseByUserId(userId);
            var dtos = apiKeys.Select(x => _mapper.Map<ApiKeyDto>(x)).ToList();

            return dtos;
        }

        public async Task<ApiKeyDto> GetAsync(string key)
        {
            var apiKeyValue = await _repository.GetByKeyAsync(key);
            if (apiKeyValue.HasNoValue)
                throw new ServiceException($"Desired API key does not exist! Key: {key}.");

            var dto = _mapper.Map<ApiKeyDto>(apiKeyValue.Value);

            return dto;
        }

        public async Task<ApiKeyDto> GetAsync(Guid id)
        {
            var apiKeyValue = await _repository.GetAsync(id);
            if (apiKeyValue.HasNoValue)
                throw new ServiceException($"Desired API key does not exist! Id: {id}.");

            var dto = _mapper.Map<ApiKeyDto>(apiKeyValue.Value);

            return dto;
        }

        public async Task CreateAsync(Guid id, Guid userId)
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
            var apiKeyValue = await _repository.GetByKeyAsync(key);
            if (apiKeyValue.HasNoValue)
                throw new ServiceException($"Desired API key does not exist! Key: {key}.");

            await _repository.DeleteAsync(apiKeyValue.Value);
        }
    }
}