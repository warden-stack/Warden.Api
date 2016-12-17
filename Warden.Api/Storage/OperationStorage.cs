using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Api.Storage
{
    public class OperationStorage : IOperationStorage
    {
        private readonly IStorageClient _storageClient;

        public OperationStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<OperationDto>> GetAsync(Guid requestId)
            => await _storageClient.GetAsync<OperationDto>($"operations/{requestId}");
    }
}