using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetAccount : IAuthenticatedQuery
    {
        public string UserId { get; set; }
    }
}