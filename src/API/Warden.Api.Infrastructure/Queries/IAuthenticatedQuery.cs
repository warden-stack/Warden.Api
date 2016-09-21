using System;

namespace Warden.Api.Infrastructure.Queries
{
    public interface IAuthenticatedQuery : IQuery
    {
        Guid AuthenticatedUserId { get; set; }
    }
}