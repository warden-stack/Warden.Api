﻿using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Api.Core.Domain;

namespace Warden.Api.Infrastructure.DTO
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public bool AutoRegisterNewWarden { get; set; }
        public IEnumerable<UserInOrganizationDto> Users { get; set; }
        public IEnumerable<WardenDto> Wardens { get; set; }

        public OrganizationDto()
        {
        }

        public OrganizationDto(Organization organization)
        {
            Id = organization.Id;
            OwnerId = organization.OwnerId;
            Name = organization.Name;
            AutoRegisterNewWarden = organization.AutoRegisterNewWarden;
            Users = organization.Users.Select(x => new UserInOrganizationDto(x));
            Wardens = organization.Wardens.Select(x => new WardenDto(x));
        }
    }
}