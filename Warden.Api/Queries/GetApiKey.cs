using System;
using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetApiKey : IAuthenticatedQuery
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}