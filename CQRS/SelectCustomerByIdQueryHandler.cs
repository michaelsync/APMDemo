using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace CQRS {
    public class SelectCustomerByIdQueryHandler : IMultipleQueriesHandler<SelectCustomerByIdQuery, IEnumerable<Customer>>
    {
        public IEnumerable<Customer> Handle(IEnumerable<SelectCustomerByIdQuery> queries)
        {
            var ids = queries.Select(a => a.Id).ToArray();
            var customerProcessor = ServiceLocator.Current.GetInstance<ICustomerRepository>();
            Console.WriteLine("Batch but not bitch!");
            return customerProcessor.GetCustomersByIds(ids);
        }
    }
}
