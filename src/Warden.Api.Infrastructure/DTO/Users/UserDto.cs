using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Users;

namespace Warden.Api.Infrastructure.DTO.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public Guid RecentlyViewedOrganizationId { get; set; }
        public Guid RecentlyViewedWardenId { get; set; }
        public IEnumerable<string> ApiKeys { get; set; }

        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role;
            RecentlyViewedOrganizationId = user.RecentlyViewedOrganizationId;
            RecentlyViewedWardenId = user.RecentlyViewedWardenId;
            ApiKeys = Enumerable.Empty<string>();
        }
    }
}