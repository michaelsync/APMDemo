using System.Collections.Generic;
using System.Linq;

namespace CQRS {
    public class CustomerRepository : ICustomerRepository {
        public IEnumerable<Customer> GetCustomers() {
            var customers = new List<Customer>{
                new Customer() { Id = 1, Name = "Michael Sync"},
                new Customer() { Id = 2, Name = "Elena Sync"}
            };
            return customers;
        }


        public IEnumerable<Customer> GetCustomersByIds(IList<int> ids)
        {
            return ids.Select(id => new Customer() {Id = id, Name = "Dummy Name"}).ToList();
        }
    }

}
