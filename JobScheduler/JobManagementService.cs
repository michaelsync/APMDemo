﻿using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.DI.AutoFac;
using Akka.Routing;
using BackEndJobs.Actors;
using JobManager.Actors;
using JobManager.Messages;
using Serilog;
using System.Configuration;

namespace JobManager {

    public class JobManagementService {
        
        private static ActorSystem system;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Start() {
            var propsResolver = InitDependencyInjection();
            ScheduleBackEndJobCoordinator(propsResolver);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static AutoFacDependencyResolver InitDependencyInjection() {
            Log.Information("Started injecting the required services and actors ");

            var builder = new Autofac.ContainerBuilder();
            //builder.RegisterType<WorkerService>().As<IWorkerService>();
            var container = builder.Build();

            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");

            system = ActorSystem.Create("MyBackendProcessingSystem", section.AkkaConfig);

            return new AutoFacDependencyResolver(container, system);
        }
        private static void ScheduleBackEndJobCoordinator(AutoFacDependencyResolver propsResolver) {
            Log.Information("ScheduleBackEndJobCoordinator");

            var backEndJobCoordinationActor = system.ActorOf(propsResolver.Create<BackEndJobCoordinationActor>(), 
                "BackEndJobCoordinationActor");

            system.Scheduler
                .ScheduleTellRepeatedly(100, 3000, backEndJobCoordinationActor, 
                new JobConfigLoadOrUpdateMessage(), backEndJobCoordinationActor);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void Stop() {
            system.Shutdown();
        }
    }
}
