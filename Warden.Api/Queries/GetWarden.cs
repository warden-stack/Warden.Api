using System;
using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetWarden : IAuthenticatedQuery
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public string UserId { get; set; }
    }
}