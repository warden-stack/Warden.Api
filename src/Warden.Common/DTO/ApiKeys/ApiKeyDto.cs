using System;

namespace Warden.Common.DTO.ApiKeys
{
    public class ApiKeyDto
    {
        public string Key { get; set; }
        public Guid UserId { get; set; }
    }
}