using System;
using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Services.Storage.Queries
{
    public class BrowseWardenCheckResults : PagedQueryBase
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
    }
}