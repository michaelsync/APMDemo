using System.Collections.Generic;

namespace CQRS {
    public interface ICustomerRepository {
        IEnumerable<Customer> GetCustomers();
    }
}
