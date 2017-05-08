using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Api.Queries;
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

        public async Task<Maybe<PagedResult<Organization>>> BrowseAsync(BrowseOrganizations query)
            => await _storageClient.GetFilteredCollectionAsync<Organization, BrowseOrganizations>(query, $"organizations");

        public async Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId)
            => await _storageClient.GetAsync<Organization>($"organizations/{organizationId}?userId={userId}");

        public async Task<Maybe<Warden.Services.Storage.Models.Organizations.Warden>> GetWardenAsync(string userId, Guid organizationId, Guid wardenId)
        {
            var organization = await GetAsync(userId, organizationId);
            if(organization.HasNoValue)
            {
                return null;
            }

            return organization.Value.Wardens?.FirstOrDefault(x => x.Id == wardenId);
        }
  }
}