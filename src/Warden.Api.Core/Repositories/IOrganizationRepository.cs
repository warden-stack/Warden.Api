using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task<PagedResult<Organization>> BrowseAsync(Guid userId, Guid ownerId,
            int page = 1, int results = 10);
        Task<Maybe<Organization>> GetAsync(Guid organizationId);
        Task<Maybe<Organization>> GetAsync(string name, Guid ownerId);
        Task UpdateAsync(Organization organization);
        Task AddAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}