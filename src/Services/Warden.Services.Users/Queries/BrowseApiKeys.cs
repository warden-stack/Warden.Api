using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Services.Users.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}