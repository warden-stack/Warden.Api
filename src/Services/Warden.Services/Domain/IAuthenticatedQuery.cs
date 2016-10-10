using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Services.Domain
{
    public interface IAuthenticatedQuery : IQuery
    {
        string AuthenticatedUserId { get; set; }
    }
}