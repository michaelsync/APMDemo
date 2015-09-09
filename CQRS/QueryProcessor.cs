using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;

namespace CQRS {
    public sealed class QueryProcessor : IQueryProcessor
    {
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = ServiceLocator.Current.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }


    }

}
