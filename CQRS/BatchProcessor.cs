using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace CQRS
{
    public class BatchProcessor : IBatchProcessor
    {
        public TResult Process<TResult>(IEnumerable<IQuery<TResult>> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
               .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = ServiceLocator.Current.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}