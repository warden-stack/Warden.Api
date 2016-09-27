using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Organizations;
using Warden.Common.Types;
using Warden.Services.Domain;

namespace Warden.Services.Organizations.Services
{
    public interface IOrganizationService
    {
        Task<PagedResult<OrganizationDto>> BrowseAsync(string userId);
        Task<Maybe<OrganizationDto>> GetAsync(Guid id);
        Task UpdateAsync(Guid id, string name, string userId);
        Task CreateAsync(string userId, string name, string description = "");
        Task CreateDefaultAsync(string userId);
        Task DeleteAsync(Guid id, string userId);
        
        Task AssignUserAsync(Guid organizationId, string email, string userId);
        Task UnassignUserAsync(Guid organizationId, string email, string userId);
        string DefaultOrganizationName { get; }
    }
}