using Akka.Actor;
using Akka.Cluster.Routing;
using Akka.Configuration.Hocon;
using Akka.DI.Ninject;
using Akka.Routing;
using BackendJobA.Actors;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendJobA {
    public class BackEndJobAService {
        private static ActorSystem system;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            var propsResolver = InitDependencyInjection();
            system.ActorOf(propsResolver.Create<BackEndJobAActor>().WithRouter(FromConfig.Instance), "BackEndJobAActor");
            //var router = ActorSystem.ActorOf(Props.Create(() => new RemoteJobActor()).WithRouter(FromConfig.Instance), "tasker");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static NinjectDependencyResolver InitDependencyInjection() {
            Log.Information("Started injecting the required services and actors ");

            var container = new Ninject.StandardKernel();
            //container.Bind<IDatabaseContextRepository>().To(typeof(MockDatabaseContextRepository));

            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            system = ActorSystem.Create("MyBackendProcessingSystem", section.AkkaConfig);

            return new NinjectDependencyResolver(container, system);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
        }
    }
}
