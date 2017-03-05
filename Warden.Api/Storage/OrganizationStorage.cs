using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Api.Storage
{
    public class OrganizationStorage : IOrganizationStorage
    {
        private readonly IStorageClient _storageClient;

        public OrganizationStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId)
            => await _storageClient.GetAsync<Organization>($"organizations/{organizationId}?userId={userId}");
    }
}