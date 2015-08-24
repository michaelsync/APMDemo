using Akka.Actor;
using Akka.DI.Core;
using JobScheduler.Messages;
using JobScheduler.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace JobScheduler.Actors {
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

                //var databaseConfigurationProps = Context.DI().Props<DatabaseConfigurationActor>();
                //var databaseConfigurationActorRef = Context.ActorOf(databaseConfigurationProps, "DatabaseConfigurationActor");
                //databaseConfigurationActorRef.Tell(new GetAllJobConfigurationsFromDbMessage());
            }
        }
        }
    
}
