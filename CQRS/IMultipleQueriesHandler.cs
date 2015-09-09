using System.Collections.Generic;

namespace CQRS
{
    public interface IMultipleQueriesHandler<in TQuery, out TResult> where TQuery: IQuery<TResult>
    {
        TResult Handle(IEnumerable<TQuery> queries);
    }
}