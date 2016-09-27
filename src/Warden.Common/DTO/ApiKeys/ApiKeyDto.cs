using System;

namespace Warden.Common.DTO.ApiKeys
{
    public class ApiKeyDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
    }
}