using System;
using Warden.Api.Core.Domain.Security;

namespace Warden.Api.Infrastructure.DTO.Security
{
    public class SecuredRequestDto
    {
        public ResourceType ResourceType { get; set; }
        public Guid ResourceId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UsedAt { get; set; }

        public SecuredRequestDto()
        {
        }

        public SecuredRequestDto(SecuredRequest request)
        {
            ResourceType = request.ResourceType;
            ResourceId = request.ResourceId;
            Token = request.Token;
            CreatedAt = request.CreatedAt;
            UsedAt = request.UsedAt;
        }
    }
}