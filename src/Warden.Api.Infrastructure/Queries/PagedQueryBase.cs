namespace Warden.Api.Infrastructure.Queries
{
    public abstract class PagedQueryBase : IQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
    }
}