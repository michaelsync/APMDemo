using Akka.Actor;
using JobScheduler.Messages;
using Serilog;

namespace JobScheduler.Actors {
    public class BackEndJobCoordinationActor : ReceiveActor {

        

        public BackEndJobCoordinationActor() {
            
            

            Receive<JobConfigLoadOrUpdateMessage>(m => {
                Log.Information("Recieved JobConfigLoadOrUpdate Request");

                //var databaseConfigurationActor = Context.ActorOf<DatabaseConfigurationActor>("DatabaseConfigurationActor");
                //databaseConfigurationActor.Tell(new GetAllJobConfigurationsFromDbMessage());
            });
        }
    }
}
