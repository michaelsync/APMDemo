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

            var backEndJobCoordinationActor = system.ActorOf(propsResolver.Create<BackEndJobCoordinationActor>(), 
                "BackEndJobCoordinationActor");

            system.Scheduler
                .ScheduleTellRepeatedly(100, 3000, backEndJobCoordinationActor, 
                new JobConfigLoadOrUpdateMessage(), backEndJobCoordinationActor);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
        }
    }
}
