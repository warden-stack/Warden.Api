using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.DTO.Security;

namespace Warden.Api.Infrastructure.Services
{
    public interface ISecuredRequestService
    {
        Task<Maybe<SecuredRequestDto>> GetAsync(SecuredResourceType resourceType, Guid resourceId);
        Task CreateAsync(SecuredResourceType resourceType, Guid resourceId);
        Task ConsumeAsync(SecuredResourceType resourceType, Guid resourceId, string token);
    }
}