using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using System;

namespace CQRS {
    public class BatchProcessor : IBatchProcessor {
        public TResult Process<TResult>(IEnumerable<IQuery<TResult>> query) {
            if (query.Any()) {
                var firstQueryType = query.First().GetType(); //query.GetType().GetGenericArguments()[0]

                var handlerType = typeof(IMultipleQueriesHandler<,>)
               .MakeGenericType(query.GetType(), typeof(TResult));

                dynamic handler = ServiceLocator.Current.GetInstance(handlerType);

                return handler.Handle((dynamic)query);
            }
            else {
                return default(TResult);
            }

        }
    }
}