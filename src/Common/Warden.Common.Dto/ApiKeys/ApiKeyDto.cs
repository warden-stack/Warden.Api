using System;

namespace Warden.Common.Dto.ApiKeys
{
    public class ApiKeyDto
    {
        public string Key { get; set; }
        public Guid UserId { get; set; }
    }
}