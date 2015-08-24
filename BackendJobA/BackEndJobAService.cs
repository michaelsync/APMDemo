using Akka.Actor;
using Akka.DI.Ninject;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendJobA {
    public class BackEndJobAService {
        private static ActorSystem system;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            InitDependencyInjection();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static NinjectDependencyResolver InitDependencyInjection() {
            Log.Information("Started injecting the required services and actors ");

            var container = new Ninject.StandardKernel();
            //container.Bind<IDatabaseContextRepository>().To(typeof(MockDatabaseContextRepository));

            system = ActorSystem.Create("MyBackendProcessingSystem");
            return new NinjectDependencyResolver(container, system);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
        }
    }
}
