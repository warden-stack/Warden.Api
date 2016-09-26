using System;

namespace Warden.Api.Infrastructure.Queries
{
    public interface IAuthenticatedQuery : IQuery
    {
        string AuthenticatedUserId { get; set; }
    }
}