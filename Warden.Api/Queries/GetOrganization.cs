using System;
using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetOrganization : IAuthenticatedQuery
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}