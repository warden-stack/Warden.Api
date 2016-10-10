using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Services.Storage.Queries
{
    public class BrowseApiKeys : PagedQueryBase
    {
        public string UserId { get; set; }
    }
}