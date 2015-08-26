using Akka.Actor;
using Akka.Cluster.Routing;
using Akka.DI.Core;
using Akka.Routing;
using BackEndJobs.Actors;
using JobManager.Messages;
using JobManager.Models;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;

namespace JobManager.Actors {
    public class BackEndJobCoordinationActor : ReceiveActor {

        public BackEndJobCoordinationActor() {

            Receive<JobConfigLoadOrUpdateMessage>(m => {
                OnJobConfigLoadOrUpdateMessageReceived();
            });

            Receive<List<JobConfigurationModel>>(m => {
                OnJobConfigurationModelsReceived(m);
            });
            
        }


        private static void OnJobConfigLoadOrUpdateMessageReceived() {

            Log.Information("Recieved JobConfigLoadOrUpdate Request");

            var databaseConfigurationProps = Context.DI().Props<DatabaseConfigurationActor>();
            var databaseConfigurationActorName = "DatabaseConfigurationActor";

            var databaseConfigurationActorRef = Context.Child(databaseConfigurationActorName).Equals(ActorRefs.Nobody)
                ? Context.ActorOf(databaseConfigurationProps, databaseConfigurationActorName)
                : Context.Child(databaseConfigurationActorName);
            databaseConfigurationActorRef.Tell(new GetAllJobConfigurationsFromDbMessage());            
        }

        private static void OnJobConfigurationModelsReceived(List<JobConfigurationModel> models) {
            Log.Information("Recieved the list of JobConfigurationModels");

            foreach(var model in models) {
                Log.Debug(model.Name);

                var router = Context.ActorOf(Props.Create(() => new BackEndJobAActor())
                .WithRouter(FromConfig.Instance),
                "BackEndJobAActor");
                router.Tell("Yell");

                //Context.ActorSelection("/user/BackEndJobAActor").Tell("Hejarr");

                //var backendRouter =
                //    Context.ActorOf(
                //        Props.Empty.WithRouter(new ClusterRouterGroup(new ConsistentHashingGroup("/user/BackEndJobAActor"),
                //            new ClusterRouterGroupSettings(10, false, "backend", ImmutableHashSet.Create("/user/BackEndJobAActor")))));

                //backendRouter.Tell("Yo yo!");
            }
        }
        }
    
}
