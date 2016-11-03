using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Services.Storage.Queries
{
    public class BrowseOrganizations : PagedQueryBase
    {
        public string UserId { get; set; }
        public string OwnerId { get; set; }
    }
}