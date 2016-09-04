using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface ISecuredRequestRepository
    {
        Task<Maybe<SecuredRequest>> GetByResourceTypeAndIdAsync(ResourceType resourceType, Guid resourceId);
        Task AddAsync(SecuredRequest securedRequest);
        Task UpdateAsync(SecuredRequest securedRequest);
    }
}