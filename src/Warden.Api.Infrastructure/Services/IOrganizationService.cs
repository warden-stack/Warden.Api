using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Infrastructure.DTO.Organizations;

namespace Warden.Api.Infrastructure.Services
{
    public interface IOrganizationService
    {
        Task<PagedResult<OrganizationDto>> BrowseAsync(Guid userId);
        Task<OrganizationDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, string name, Guid authenticatedUserId);
        Task CreateAsync(Guid userId, string name);
        Task DeleteAsync(Guid id, Guid authenticatedUserId);
        
        Task AssignUserAsync(Guid organizationId, string email, Guid authenticatedUserId);
    }
}