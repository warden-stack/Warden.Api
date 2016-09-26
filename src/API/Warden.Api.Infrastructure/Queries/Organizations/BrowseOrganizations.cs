using System;

namespace Warden.Api.Infrastructure.Queries.Organizations
{
    public class BrowseOrganizations : PagedQueryBase
    {
        public string UserId { get; set; }
        public string OwnerId { get; set; }
    }
}