using System;
using System.Collections.Generic;
using Warden.DTO.Wardens;

namespace Warden.DTO.Organizations
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<UserInOrganizationDto> Users { get; set; }
        public IList<WardenDto> Wardens { get; set; }
    }
}