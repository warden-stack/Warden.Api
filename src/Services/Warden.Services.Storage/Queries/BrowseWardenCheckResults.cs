using System;
using Warden.Common.Types;
using Warden.Services.Domain;

namespace Warden.Services.Storage.Queries
{
    public class BrowseWardenCheckResults : PagedQueryBase
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
    }
}