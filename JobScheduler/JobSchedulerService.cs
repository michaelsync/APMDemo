using Akka.Actor;
using Akka.DI.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class JobSchedulerService {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            InitDependencyInjection();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static void InitDependencyInjection() {
            var container = new Ninject.StandardKernel();
            container.Bind<IDatabaseContextRepository>().To(typeof(MockDatabaseContextRepository));

            var system = ActorSystem.Create("MyBackendProcessingSystem");
            var propsResolver = new NinjectDependencyResolver(container, system);
                        
            var backendJobConfigurationActor = system.ActorOf(propsResolver.Create<BackEndJobConfigurationActor>(), "BackendJobConfigurationActor");
            
            backendJobConfigurationActor.Tell("Start");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
        }
    }
}
