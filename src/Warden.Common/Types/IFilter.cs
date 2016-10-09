using System.Collections.Generic;

namespace Warden.Common.Types
{
    public interface IFilter<TResult, in TQuery> where TQuery : IQuery
    {
        IEnumerable<TResult> Filter(IEnumerable<TResult> values, TQuery query);
    }
}