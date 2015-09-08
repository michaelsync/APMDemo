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
}
