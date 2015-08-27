using Akka.Actor;
using Akka.DI.Core;
using Akka.Monitoring;
using Akka.Routing;
using BackEndSystem.Common;
using BackEndSystem.Common.Messages;
using JobManager.Messages;
using JobManager.Models;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace JobManager.Actors {
    public class BackEndJobCoordinationActor : LogEnabledRecieveActor {

        IActorRef trackingRouter;
        public BackEndJobCoordinationActor(IActorRef router) {
            this.trackingRouter = router;

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
                using (LogContext.PushProperty("JobId", model.Id)) {

                    var childActorName = string.Format(CultureInfo.InvariantCulture, "backend{0}", model.Id);

                    var router = Context.ActorOf(Props.Create(() => new EmptyRemoteJobActor())
                        .WithRouter(FromConfig.Instance), childActorName);

                    router.Tell(new StartBackEndJobMessage(model.Id));

                    Log.Debug(model.Name);
                    Log.Debug("Kick off new job");
                    //Console.ReadLine();
                    this.trackingRouter.Tell(new StartBackEndJobMessage(model.Id));
                }                    
            }
        }
   }
    
}
