using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Maybe<Organization>> GetAsync(Guid organizationId);
        Task<Maybe<Organization>> GetAsync(string name, string ownerId);
        Task AddAsync(Organization organization);

        Task<PagedResult<Organization>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10);
    }
}