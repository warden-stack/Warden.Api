using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Operations;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.Settings;

namespace Warden.Services.Storage.Providers
{
    public class OperationProvider : IOperationProvider
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IProviderClient _providerClient;
        private readonly ProviderSettings _providerSettings;

        public OperationProvider(IOperationRepository operationRepository,
            IProviderClient providerClient,
            ProviderSettings providerSettings)
        {
            _operationRepository = operationRepository;
            _providerClient = providerClient;
            _providerSettings = providerSettings;
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId) =>
            await _providerClient.GetUsingStorageAsync(_providerSettings.OperationsApiUrl,
                $"/operations/{requestId}", async () => await _operationRepository.GetAsync(requestId),
                async operation => await _operationRepository.AddAsync(operation));
    }
}