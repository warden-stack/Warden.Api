using System;
using System.Collections.Generic;
using Warden.Common.Dto.Users;
using Warden.Common.Dto.Wardens;

namespace Warden.Common.Dto.Organizations
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