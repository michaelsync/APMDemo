using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.DI.AutoFac;
using Autofac;
using BackEndJobs.Actors;
using Serilog;
using System.Configuration;

namespace BackEndJobContainer {
    public class BackEndJobService {
        private static ActorSystem system;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            InitDependencyInjection();                        
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static AutoFacDependencyResolver InitDependencyInjection() {
            Log.Information("Started injecting the required services and actors ");

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<BackEndJobAActor>();
            var container = builder.Build();

            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            system = ActorSystem.Create("MyBackendProcessingSystem", section.AkkaConfig);
            system.ActorOf(Props.Create(() => new BackEndJobAActor()), "bankends");

            //var propsResolver = new AutoFacDependencyResolver(container, system);            

            //system.ActorOf(propsResolver.Create<BackEndJobAActor>(), "backends");

            //return propsResolver;
            return null;
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
            system.Shutdown();
        }
    }
}
