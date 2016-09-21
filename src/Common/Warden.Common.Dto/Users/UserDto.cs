using System;
using System.Collections.Generic;

namespace Warden.Common.Dto.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public Guid RecentlyViewedOrganizationId { get; set; }
        public Guid RecentlyViewedWardenId { get; set; }
        public IEnumerable<string> ApiKeys { get; set; }
    }
}