using System.Collections.Generic;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Api.Filters
{
    public class BrowseUsersFilter : IFilter<User, BrowseUsers>
    {
        public IEnumerable<User> Filter(IEnumerable<User> values, BrowseUsers query)
        {
            return values;
        }
    }
}