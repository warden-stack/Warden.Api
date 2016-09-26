namespace Warden.Api.Core.Queries
{
    public interface IAuthenticatedQuery : IQuery
    {
        string AuthenticatedUserId { get; set; }
    }
}