using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task AddAsync(Organization organization);
        Task<Maybe<Organization>> GetAsync(Guid organizationId);
        Task<Maybe<Organization>> GetAsync(string name, Guid ownerId);

        Task<PagedResult<Organization>> BrowseAsync(Guid userId, Guid ownerId,
            int page = 1, int results = 10);

        Task UpdateAsync(Organization organization);
    }
}