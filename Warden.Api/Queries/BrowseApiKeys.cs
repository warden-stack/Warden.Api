using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}