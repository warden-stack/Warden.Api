﻿using System.Collections.Generic;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Filters
{
    public class BrowseUsersFilter : IFilter<UserDto, BrowseUsers>
    {
        public IEnumerable<UserDto> Filter(IEnumerable<UserDto> values, BrowseUsers query)
        {
            return values;
        }
    }
}