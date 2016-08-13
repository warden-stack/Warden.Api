using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Infrastructure.DTO.Organizations;

namespace Warden.Api.Infrastructure.Services
{
    public interface IOrganizationService
    {
        Task CreateAsync(Guid userId, string name);
        Task<PagedResult<OrganizationDto>> BrowseAsync(Guid userId);
        Task<OrganizationDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, string name);

        Task AddUserAsync(Guid id, string email);
    }
}