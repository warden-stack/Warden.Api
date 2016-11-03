using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;

namespace Warden.Services.Organizations.Services
{
    public interface IOrganizationService
    {
        Task<Maybe<Organization>> GetAsync(Guid id);
        Task<Maybe<PagedResult<Organization>>> BrowseAsync(string userId);
        Task UpdateAsync(Guid id, string name, string userId);
        Task CreateAsync(Guid id, string userId, string name, string description = "");
        Task CreateDefaultAsync(Guid id, string userId);
        Task DeleteAsync(Guid id, string userId);
        Task AssignUserAsync(Guid organizationId, string userId, string email, string role);
        Task UnassignUserAsync(Guid organizationId, string userId);
        string DefaultOrganizationName { get; }
    }
}