using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;

namespace CQRS {
    public class SelectCustomerQueryHandler : IQueryHandler<SelectCustomerQuery, IEnumerable<Customer>> {
        public IEnumerable<Customer> Handle(SelectCustomerQuery query) {
            var customerProcessor = ServiceLocator.Current.GetInstance<ICustomerRepository>();
            Console.WriteLine("d");
            return customerProcessor.GetCustomers();
        }
    }
}
