using System.Threading.Tasks;
using Warden.Common.DTO.Organizations;
using Warden.Common.Types;
using Warden.Services.Domain;

namespace Warden.Services.Storage.Repositories
{
    public interface IOrganizationRepository
    {
        Task<PagedResult<OrganizationDto>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10);

        Task<Maybe<OrganizationDto>> GetAsync(string userId, string name);
        Task AddAsync(OrganizationDto organization);
        Task UpdateAsync(OrganizationDto organization);
        Task DeleteAsync(OrganizationDto organization);
    }
}