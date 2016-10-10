using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Api.Core.Filters
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}