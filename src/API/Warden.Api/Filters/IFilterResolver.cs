using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Api.Filters
{
    public interface IFilterResolver
    {
        IFilter<TResult, TQuery> Resolve<TResult, TQuery>() where TQuery : IQuery;
    }
}