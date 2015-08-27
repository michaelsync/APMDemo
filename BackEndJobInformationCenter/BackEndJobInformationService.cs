using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.DI.AutoFac;
using Akka.Monitoring;
using Akka.Monitoring.PerformanceCounters;
using Autofac;
using BackEndJobInformationCenter.Actors;
using Serilog;
using System.Configuration;

namespace BackEndJobInformationCenter {
    public class BackEndJobInformationService {
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

            system.ActorOf(Props.Create(() => new BackEndJobAActor(1)), "bankends");

            ActorMonitoringExtension.RegisterMonitor(system,
                new ActorPerformanceCountersMonitor());
            
            return null;
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
            system.Shutdown();
        }
    }
}
