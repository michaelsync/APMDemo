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
            //SingleQueryDemo.Run();
            BulkQueryDemo.Run();

            Console.ReadLine();
        }
    }

    public static class SingleQueryDemo {
        public static void Run() {
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
        }
    }

    public static class BulkQueryDemo {
        public static void Run() {
            var builder = new ContainerBuilder();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<BatchProcessor>().As<IBatchProcessor>();

            builder.Register(c => new SelectCustomerByIdQueryHandler())
                .As<IMultipleQueriesHandler<List<SelectCustomerByIdQuery>, IEnumerable<Customer>>>();

            builder.RegisterType<Runner>();
            var container = builder.Build();

            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
            var runner = container.Resolve<Runner>();

            var queries = new List<SelectCustomerByIdQuery>
            {
                new SelectCustomerByIdQuery() { Id =1  },
                new SelectCustomerByIdQuery() { Id =2  }
            };
            runner.RunAll(queries);
        }
    }

    public class Runner {
        public void Run(IQuery<IEnumerable<Customer>> query) {
            var queryProcessor = ServiceLocator.Current.GetInstance<IQueryProcessor>();

            queryProcessor.Process(query);

        }


        public IEnumerable<Customer> RunAll(IEnumerable<IQuery<IEnumerable<Customer>>> queries) {
            var batchProcessor = ServiceLocator.Current.GetInstance<IBatchProcessor>();
            return batchProcessor.Process(queries);
        }
    }
}
