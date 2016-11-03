using Warden.Common.Queries;

namespace Warden.Common.Domain
{
    public interface IAuthenticatedQuery : IQuery
    {
        string AuthenticatedUserId { get; set; }
    }
}