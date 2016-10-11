using System.Collections.Generic;
using Warden.Api.Core.Queries;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Api.Core.Filters
{
    public class BrowseUsersFilter : IFilter<UserDto, BrowseUsers>
    {
        public IEnumerable<UserDto> Filter(IEnumerable<UserDto> values, BrowseUsers query)
        {
            return values;
        }
    }
}