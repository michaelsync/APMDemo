using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using BackEndSystem.Common.Messages;
using JobManager.Messages;
using JobManager.Models;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;

namespace JobManager.Actors {
    public class BackEndJobCoordinationActor : ReceiveActor {

        IActorRef router;
        public BackEndJobCoordinationActor(IActorRef router) {
            this.router = router;

            Receive<JobConfigLoadOrUpdateMessage>(m => {
                OnJobConfigLoadOrUpdateMessageReceived();
            });

            Receive<List<JobConfigurationModel>>(m => {
                OnJobConfigurationModelsReceived(m);
            });
            
        }

        private static void OnJobConfigLoadOrUpdateMessageReceived() {
            Log.Information("Recieved JobConfigLoadOrUpdate Request");
            IActorRef databaseConfigurationActorRef = CreateOrGetActor<DatabaseConfigurationActor>("DatabaseConfigurationActor");
            databaseConfigurationActorRef.Tell(new GetAllJobConfigurationsFromDbMessage());
        }

        private static IActorRef CreateOrGetActor<T>(string databaseConfigurationActorName, bool withRouther = false) where T : ActorBase {
            var databaseConfigurationProps = withRouther ? Context.DI().Props<T>().WithRouter(FromConfig.Instance)
                :  Context.DI().Props<T>(); 

            var databaseConfigurationActorRef = Context.Child(databaseConfigurationActorName).Equals(ActorRefs.Nobody)
                ? Context.ActorOf(databaseConfigurationProps, databaseConfigurationActorName) 
                : Context.Child(databaseConfigurationActorName);

            return databaseConfigurationActorRef;
        }

        private void OnJobConfigurationModelsReceived(List<JobConfigurationModel> models) {
            Log.Information("Recieved the list of JobConfigurationModels");

            foreach(var model in models) {
                using (LogContext.PushProperty("JobId", "1")) {
                    Log.Debug(model.Name);
                    Console.ReadLine();
                    this.router.Tell(new StartBackEndJobMessage(1));
                }                    
            }
        }
   }
    
}
