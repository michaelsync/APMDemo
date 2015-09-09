using System.Collections.Generic;

namespace CQRS
{
    public interface IMultipleQueriesHandler<in TQueries, out TResult> 
        where TQueries: IEnumerable<IQuery<TResult>>
    {
        TResult Handle(TQueries queries);
    }
}