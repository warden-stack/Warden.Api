using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Types;
using Warden.Common.DTO.Security;

namespace Warden.Api.Infrastructure.Services
{
    public interface ISecuredRequestService
    {
        Task<Maybe<SecuredRequestDto>> GetAsync(Guid id);
        Task<Maybe<SecuredRequestDto>> GetAsync(ResourceType resourceType, Guid resourceId, string token);
        Task CreateAsync(Guid id, ResourceType resourceType, Guid resourceId);
        Task ConsumeAsync(ResourceType resourceType, Guid resourceId, string token);
    }
}