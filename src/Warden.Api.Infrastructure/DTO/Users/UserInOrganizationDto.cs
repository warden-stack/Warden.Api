using System;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;

namespace Warden.Api.Infrastructure.DTO.Users
{
    public class UserInOrganizationDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public OrganizationRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserInOrganizationDto()
        {
        }

        public UserInOrganizationDto(UserInOrganization user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role;
            CreatedAt = user.CreatedAt;
        }
    }
}