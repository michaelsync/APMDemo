using Akka.Actor;
using Akka.DI.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class JobSchedulerService {
        public void Start() {
            InitDependencyInjection();
        }

        private static void InitDependencyInjection() {
            var container = new Ninject.StandardKernel();
            //container.Bind<IWorkerService>().To(typeof(WorkerService));

            var system = ActorSystem.Create("MyBackendProcessingSystem");
            var propsResolver = new NinjectDependencyResolver(container, system);
        }

        public void Stop() {
        }
    }
}
