using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.DTO.Security;

namespace Warden.Api.Infrastructure.Services
{
    public class SecuredRequestService : ISecuredRequestService
    {
        private readonly ISecuredRequestRepository _securedRequestRepository;

        public SecuredRequestService(ISecuredRequestRepository securedRequestRepository)
        {
            _securedRequestRepository = securedRequestRepository;
        }

        public async Task<Maybe<SecuredRequestDto>> GetAsync(ResourceType resourceType, Guid resourceId)
        {
            var securedRequest = await _securedRequestRepository.GetByResourceTypeAndIdAsync(resourceType, resourceId);

            return securedRequest.HasNoValue ? new Maybe<SecuredRequestDto>() : new SecuredRequestDto(securedRequest.Value);
        }

        public async Task CreateAsync(ResourceType resourceType, Guid resourceId)
        {
            var securedRequest = new SecuredRequest(resourceType, resourceId);
            await _securedRequestRepository.AddAsync(securedRequest);
        }

        public async Task ConsumeAsync(ResourceType resourceType, Guid resourceId, string token)
        {
            var securedRequest = await _securedRequestRepository.GetByResourceTypeAndIdAsync(resourceType, resourceId);
            if (securedRequest.HasNoValue)
                throw new ArgumentException("Resource has not been found for given id.");

            securedRequest.Value.Consume(token);
            await _securedRequestRepository.UpdateAsync(securedRequest.Value);
        }
    }
}