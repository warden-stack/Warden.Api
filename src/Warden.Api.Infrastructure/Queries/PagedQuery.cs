namespace Warden.Api.Infrastructure.Queries
{
    public abstract class PagedQuery : IQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
    }
}