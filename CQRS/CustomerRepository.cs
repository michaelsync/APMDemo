using System.Collections.Generic;

namespace CQRS {
    public class CustomerRepository : ICustomerRepository {
        public IEnumerable<Customer> GetCustomers() {
            var customers = new List<Customer>{
                new Customer()
            };
            return customers;
        }
    }

}
