using System;

namespace Warden.Api.Infrastructure.Queries.Organizations
{
    public class BrowseOrganizations : PagedQueryBase
    {
        public Guid UserId { get; set; }
        public Guid OwnerId { get; set; }
    }
}