using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;

namespace Warden.Api.Storage
{
    public interface IOrganizationStorage
    {
        Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId);
    }
}