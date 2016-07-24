using System;
using Warden.Api.Core.Domain;

namespace Warden.Api.Core.Queries
{
    public class BrowseOrganizations : PagedQueryBase
    {
        public Guid UserId { get; set; }
        public Guid OwnerId { get; set; }
    }
}