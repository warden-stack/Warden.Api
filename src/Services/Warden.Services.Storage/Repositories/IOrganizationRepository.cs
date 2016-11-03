using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Organizations;

namespace Warden.Services.Storage.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Maybe<PagedResult<OrganizationDto>>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10);

        Task<Maybe<OrganizationDto>> GetAsync(Guid id);
        Task<Maybe<OrganizationDto>> GetAsync(string userId, string name);
        Task AddAsync(OrganizationDto organization);
        Task UpdateAsync(OrganizationDto organization);
        Task DeleteAsync(OrganizationDto organization);
    }
}