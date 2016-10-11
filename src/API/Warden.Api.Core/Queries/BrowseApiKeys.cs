using Warden.Common.Queries;

namespace Warden.Api.Core.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}