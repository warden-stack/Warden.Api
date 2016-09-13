using System;

namespace Warden.Api.Infrastructure.DTO.ApiKeys
{
    public class ApiKeyDto
    {
        public string Key { get; set; }
        public Guid UserId { get; set; }
    }
}