using System.Collections.Generic;
using Warden.Common.Types;

namespace Warden.Api.Core.Filters
{
    public class EmptyFilter<TResult, TQuery> : IFilter<TResult, TQuery> where TQuery : IQuery
    {
        public IEnumerable<TResult> Filter(IEnumerable<TResult> values, TQuery query)
        {
            return values;
        }
    }
}