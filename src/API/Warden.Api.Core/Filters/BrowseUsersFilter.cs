using System.Collections.Generic;
using Warden.Common.Types;
using Warden.DTO.Users;

namespace Warden.Api.Core.Filters
{
    public class BrowseUsersFilter : IFilter<UserDto, BrowseUsers>
    {
        public Maybe<IEnumerable<UserDto>> Filter(Maybe<IEnumerable<UserDto>> values, BrowseUsers query)
        {
            if (values.HasNoValue)
                return new Maybe<IEnumerable<UserDto>>();
            if (query == null)
                return values;

            return values;
        }
    }
}