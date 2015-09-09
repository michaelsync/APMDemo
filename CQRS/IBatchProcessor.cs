using System.Collections.Generic;

namespace CQRS
{
    public interface IBatchProcessor
    {
        TResult Process<TResult>(IEnumerable<IQuery<TResult>> query);
    }
}