using System;
using Warden.Api.Core.Domain;

namespace Warden.Api.Infrastructure.DTO
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