using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Common.DTO.Organizations;

namespace Warden.Api.Infrastructure.Services
{
    public interface IOrganizationService
    {
        Task<PagedResult<OrganizationDto>> BrowseAsync(string userId);
        Task<OrganizationDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, string name, string userId);
        Task CreateAsync(string userId, string name, string description = "");
        Task CreateDefaultAsync(string userId);
        Task DeleteAsync(Guid id, string userId);
        
        Task AssignUserAsync(Guid organizationId, string email, string userId);
        Task UnassignUserAsync(Guid organizationId, string email, string userId);
    }
}