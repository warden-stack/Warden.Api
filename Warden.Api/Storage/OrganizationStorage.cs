using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;

namespace Warden.Api.Storage
{
    public class OrganizationStorage : IOrganizationStorage
    {
        private readonly IStorageClient _storageClient;

        public OrganizationStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId)
            => await _storageClient.GetAsync<OrganizationDto>($"organizations/{organizationId}?userId={userId}");
    }
}