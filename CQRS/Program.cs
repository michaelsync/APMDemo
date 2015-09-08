using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS {
    class Program {
        static void Main(string[] args) {
            var builder = new ContainerBuilder();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<QueryProcessor>().As<IQueryProcessor>();
            builder.Register(c => new SelectCustomerQueryHandler())
                .As<IQueryHandler<SelectCustomerQuery, IEnumerable<Customer>>>();
            builder.RegisterType<Runner>();
            var container = builder.Build();

            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);

            var runner = container.Resolve<Runner>();
            runner.Run(new SelectCustomerQuery());

            Console.ReadLine();
        }
    }

    public class Runner {
        public void Run(IQuery<IEnumerable<Customer>> query) {
            var queryProcessor = ServiceLocator.Current.GetInstance<IQueryProcessor>();

            queryProcessor.Process(query);

        }
    }

    public class SelectCustomerQuery : IQuery<IEnumerable<Customer>> {

    }

    public class SelectCustomerQueryHandler : IQueryHandler<SelectCustomerQuery, IEnumerable<Customer>> {
        public IEnumerable<Customer> Handle(SelectCustomerQuery query) {
            var customerProcessor = ServiceLocator.Current.GetInstance<ICustomerRepository>();
            Console.WriteLine("d");
            return customerProcessor.GetCustomers();
        }
    }



    public interface ICustomerRepository {
        IEnumerable<Customer> GetCustomers();
    }
    public class CustomerRepository : ICustomerRepository {
        public IEnumerable<Customer> GetCustomers() {
            var customers = new List<Customer>{
                new Customer()
            };
            return customers;
        }
    }
    public class Customer { }

    public interface IQuery<TResult> {
    }

    public interface IQueryHandler<in TQuery,out TResult> where TQuery : IQuery<TResult> {
        TResult Handle(TQuery query);        
    }
   
    public interface IQueryProcessor {
        TResult Process<TResult>(IQuery<TResult> query);
    }

    public sealed class QueryProcessor : IQueryProcessor {
        
        public TResult Process<TResult>(IQuery<TResult> query)  {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = ServiceLocator.Current.GetInstance(handlerType);

            return handler.Handle(new SelectCustomerQuery());
        }
    }


    //    //public TResult Process<TResult>(IQuery<TResult> query) {
    //    //    //var handlerType = typeof(IQueryHandler<,>)
    //    //    //    .MakeGenericType(query.GetType(), typeof(TResult));

    //    //    //dynamic handler = container.GetInstance(handlerType);

    //    //    return handler.Handle((dynamic)query);
    //    //}
    //}


}
