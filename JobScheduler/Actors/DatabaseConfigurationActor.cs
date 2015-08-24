using Akka.Actor;
using JobScheduler.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler.Actors {
    public class DatabaseConfigurationActor : ReceiveActor {
        public DatabaseConfigurationActor() {

            Receive<GetAllJobConfigurationsFromDbMessage>(m => {
                Console.WriteLine("Database");
                //var a = Context.ActorSelection("");
                //a.Tell();
            });
        }

        
     }
}
