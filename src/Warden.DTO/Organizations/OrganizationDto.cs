using System;
using System.Collections.Generic;
using Warden.DTO.Users;
using Warden.DTO.Wardens;

namespace Warden.DTO.Organizations
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AutoRegisterNewWarden { get; set; }
        public IEnumerable<UserInOrganizationDto> Users { get; set; }
        public IEnumerable<WardenDto> Wardens { get; set; }
    }
}