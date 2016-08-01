﻿using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetAsync(Guid organizationId);
        Task<Organization> GetAsync(string name, Guid ownerId);
        Task AddAsync(Organization organization);

        Task<PagedResult<Organization>> BrowseAsync(Guid userId, Guid ownerId,
            int page = 1, int results = 10);
    }
}