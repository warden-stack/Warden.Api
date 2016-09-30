using Warden.Common.Types;

namespace Warden.Api.Core.Filters
{
    public interface IFilterResolver
    {
        IFilter<TResult, TQuery> Resolve<TResult, TQuery>() where TQuery : IQuery;
    }
}