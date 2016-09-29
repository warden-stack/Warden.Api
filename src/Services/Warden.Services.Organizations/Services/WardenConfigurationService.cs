using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Repositories;

namespace Warden.Services.Organizations.Services
{
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

        public async Task<Maybe<WardenConfiguration>> GetAsync(Guid id)
        {
            var configuration = await _wardenConfigurationRepository.GetAsync(id);
            if (configuration.HasNoValue)
                return new Maybe<WardenConfiguration>();

            var deserializedConfiguration = JsonConvert.DeserializeObject(configuration.Value.Configuration);

            return configuration.Value;
        }
    }
}