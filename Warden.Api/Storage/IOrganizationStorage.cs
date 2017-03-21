using System;
using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Api.Storage
{
    public interface IOrganizationStorage
    {
        Task<Maybe<PagedResult<Organization>>> BrowseAsync(BrowseOrganizations query);
        Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId);
    }
}