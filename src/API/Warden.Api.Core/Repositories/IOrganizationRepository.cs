﻿using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Organizations;
using Warden.Common.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IOrganizationRepository
    {
        Task<PagedResult<Organization>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10);
        Task<Maybe<Organization>> GetAsync(Guid id);
        Task<Maybe<Organization>> GetAsync(string name, string ownerId);
        Task UpdateAsync(Organization organization);
        Task AddAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}