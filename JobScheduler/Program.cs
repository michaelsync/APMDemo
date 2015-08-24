using Akka.Actor;
using Akka.DI.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace JobScheduler {
    class Program {
        static void Main(string[] args) {

            HostFactory.Run(x =>                                 //1
            {
                x.Service<JobSchedulerService>(s =>                        //2
                {
                    s.ConstructUsing(name => new JobSchedulerService());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Sample Topshelf Host");        //7
                x.SetDisplayName("Stuff");                       //8
                x.SetServiceName("Stuff");                       //9
            });

            // Create and build your container
            var container = new Ninject.StandardKernel();
            //container.Bind<TypedWorker>().To(typeof(TypedWorker));
            //container.Bind<IWorkerService>().To(typeof(WorkerService));

            // Create the ActorSystem and Dependency Resolver
            var system = ActorSystem.Create("MySystem");
            var propsResolver = new NinjectDependencyResolver(container, system);
        }
    }
}
