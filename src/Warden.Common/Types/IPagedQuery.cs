namespace Warden.Common.Types
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; }
        int Results { get; }
    }
}