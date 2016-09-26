using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Common.Types;

namespace Warden.Api.Core.Repositories
{
    public interface ISecuredRequestRepository
    {
        Task<Maybe<SecuredRequest>> GetAsync(Guid id);
        Task<Maybe<SecuredRequest>> GetByResourceTypeAndIdAndTokenAsync(ResourceType resourceType, Guid resourceId,
            string token);

        Task AddAsync(SecuredRequest securedRequest);
        Task UpdateAsync(SecuredRequest securedRequest);
    }
}