using System.Collections.Generic;

namespace Warden.Common.Types
{
    public interface IFilter<TResult, in TQuery> where TQuery : IQuery
    {
        Maybe<IEnumerable<TResult>> Filter(Maybe<IEnumerable<TResult>> values, TQuery query);
    }
}