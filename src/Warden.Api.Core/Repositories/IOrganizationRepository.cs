using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Queries;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetAsync(Guid organizationId);
        Task<Organization> GetAsync(string name, Guid ownerId);
        Task AddAsync(Organization organization);

        IQueryable<Organization> Browse(BrowseOrganizations query);
    }
}