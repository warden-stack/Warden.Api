using System;
using System.Collections.Generic;
using Warden.Common.DTO.Users;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.DTO.Organizations
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AutoRegisterNewWarden { get; set; }
        public IEnumerable<UserInOrganizationDto> Users { get; set; }
        public IEnumerable<WardenDto> Wardens { get; set; }
    }
}