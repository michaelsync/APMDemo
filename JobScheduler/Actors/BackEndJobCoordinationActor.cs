using Akka.Actor;
using JobScheduler.Messages;

namespace JobScheduler.Actors {
    public class BackEndJobCoordinationActor : ReceiveActor {
        
        public BackEndJobCoordinationActor() {
            

            Receive<StartJobCoordinatorMessage>(m => {
                var databaseConfigurationActor = Context.ActorOf<DatabaseConfigurationActor>("DatabaseConfigurationActor");
                databaseConfigurationActor.Tell(new GetAllJobConfigurationsFromDbMessage());
            });
        }
    }
}
