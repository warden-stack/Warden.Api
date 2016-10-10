using Warden.Common.Queries;
using Warden.Common.Types;
using Warden.Services.Domain;

namespace Warden.Services.Organizations.Queries
{
    public class BrowseOrganizations : PagedQueryBase
    {
        public string UserId { get; set; }
        public string OwnerId { get; set; }
    }
}