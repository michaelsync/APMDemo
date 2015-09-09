using System.Collections.Generic;

namespace CQRS
{
    public interface IMultipleQueriesHandler<in TQueryList, out TResult> 
    {
        TResult Handle(TQueryList query);
    }
}