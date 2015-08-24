using Akka.Actor;
using Akka.DI.Ninject;
using JobScheduler.Actors;
using JobScheduler.Messages;
using Serilog;
using System;

namespace JobScheduler {
    public class JobSchedulerService {
        
        private static ActorSystem system;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            var propsResolver = InitDependencyInjection();
            ScheduleBackEndJobCoordinator(propsResolver);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static NinjectDependencyResolver InitDependencyInjection() {
            Log.Information("Started injecting the required services and actors ");

            var container = new Ninject.StandardKernel();
            //container.Bind<IDatabaseContextRepository>().To(typeof(MockDatabaseContextRepository));

            system = ActorSystem.Create("MyBackendProcessingSystem");
            return new NinjectDependencyResolver(container, system);            
        }

        private static void ScheduleBackEndJobCoordinator(NinjectDependencyResolver propsResolver) {
            Log.Information("ScheduleBackEndJobCoordinator");
            var backendJobConfigurationActor = system.ActorOf(propsResolver.Create<DatabaseConfigurationActor>(), "BackendJobConfigurationActor");
            system.Scheduler
                .ScheduleTellRepeatedly(100, 3000, backendJobConfigurationActor, 
                new JobConfigLoadOrUpdateMessage(), backendJobConfigurationActor);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
        }
    }
}
