using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class BackEndJobCoordinationActor : UntypedActor {

        public BackEndJobCoordinationActor() {
            Context.ActorOf<BackEndJobConfigurationActor>("DatabaseConfigurationReader");
        }
        protected override void OnReceive(object message) {
            
        }
    }
}
