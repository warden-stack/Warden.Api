using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Wardens;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Mongo.Repositories;
using Warden.Common.DTO.WardenConfigurations;
using Warden.Common.Types;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenConfigurationService
    {
        Task CreateAsync(Guid id, object configuration);
        Task<Maybe<WardenConfigurationDto>> GetAsync(Guid id);
    }

    public class WardenConfigurationService : IWardenConfigurationService
    {
        private readonly IWardenConfigurationRepository _wardenConfigurationRepository;

        public WardenConfigurationService(IWardenConfigurationRepository wardenConfigurationRepository)
        {
            _wardenConfigurationRepository = wardenConfigurationRepository;
        }

        public async Task CreateAsync(Guid id, object configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration), "Warden configuration can not be null.");

            var serializedConfiguration = JsonConvert.SerializeObject(configuration);
            var wardenConfiguration = new WardenConfiguration(id, serializedConfiguration);
            await _wardenConfigurationRepository.AddAsync(wardenConfiguration);
        }

        public async Task<Maybe<WardenConfigurationDto>> GetAsync(Guid id)
        {
            var configuration = await _wardenConfigurationRepository.GetAsync(id);
            if (configuration.HasNoValue)
                return new Maybe<WardenConfigurationDto>();

            var deserializedConfiguration = JsonConvert.DeserializeObject(configuration.Value.Configuration);

            return new WardenConfigurationDto
            {
                Configuration = deserializedConfiguration
            };
        }
    }
}