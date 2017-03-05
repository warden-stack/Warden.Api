using System;
using System.Threading.Tasks;
using Warden.Api.Domain.Security;
using Warden.Common.Types;

namespace Warden.Api.Services
{
    public interface ISecuredRequestService
    {
//        Task<Maybe<SecuredRequest>> GetAsync(Guid id);
//        Task<Maybe<SecuredRequest>> GetAsync(ResourceType resourceType, Guid resourceId, string token);
        Task CreateAsync(Guid id, ResourceType resourceType, Guid resourceId);
        Task ConsumeAsync(ResourceType resourceType, Guid resourceId, string token);
    }
}