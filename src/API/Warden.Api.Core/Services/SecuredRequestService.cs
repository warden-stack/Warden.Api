using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Repositories;
using Warden.Common.Types;
using Warden.DTO.Security;

namespace Warden.Api.Core.Services
{
    public class SecuredRequestService : ISecuredRequestService
    {
        private readonly ISecuredRequestRepository _securedRequestRepository;

        public SecuredRequestService(ISecuredRequestRepository securedRequestRepository)
        {
            _securedRequestRepository = securedRequestRepository;
        }

        public async Task<Maybe<SecuredRequestDto>> GetAsync(Guid id)
        {
            var securedRequest = await _securedRequestRepository.GetAsync(id);

            return securedRequest.HasNoValue
                ? new Maybe<SecuredRequestDto>()
                : new SecuredRequestDto
                {
                    CreatedAt = securedRequest.Value.CreatedAt,
                    Token = securedRequest.Value.Token,
                    UsedAt = securedRequest.Value.UsedAt,
                    ResourceId = securedRequest.Value.ResourceId,
                    ResourceType = securedRequest.Value.ResourceType.ToString()
                };
        }

        public async Task<Maybe<SecuredRequestDto>> GetAsync(ResourceType resourceType, Guid resourceId, string token)
        {
            var securedRequest = await _securedRequestRepository
                .GetByResourceTypeAndIdAndTokenAsync(resourceType, resourceId, token);

            return securedRequest.HasNoValue
                ? new Maybe<SecuredRequestDto>()
                : new SecuredRequestDto
                {
                    CreatedAt = securedRequest.Value.CreatedAt,
                    Token = securedRequest.Value.Token,
                    UsedAt = securedRequest.Value.UsedAt,
                    ResourceId = securedRequest.Value.ResourceId,
                    ResourceType = securedRequest.Value.ResourceType.ToString()
                };
        }

        public async Task CreateAsync(Guid id, ResourceType resourceType, Guid resourceId)
        {
            var securedRequest = new SecuredRequest(id, resourceType, resourceId);
            await _securedRequestRepository.AddAsync(securedRequest);
        }

        public async Task ConsumeAsync(ResourceType resourceType, Guid resourceId, string token)
        {
            var securedRequest =
                await _securedRequestRepository.GetByResourceTypeAndIdAndTokenAsync(resourceType, resourceId, token);
            if (securedRequest.HasNoValue)
                throw new ArgumentException("Resource has not been found for given id.");

            securedRequest.Value.Consume(token);
            await _securedRequestRepository.UpdateAsync(securedRequest.Value);
        }
    }
}