using System.Collections.Generic;

namespace CQRS
{
    public interface IMultipleQueriesHandler<in TQueryList, in TQuery, out TResult> 
        where TQuery : IQuery<TResult>
        where TQueryList : IList<TQuery>
    {
        TResult Handle(TQueryList query);
    }
}