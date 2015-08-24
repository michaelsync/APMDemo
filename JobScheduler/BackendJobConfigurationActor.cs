using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class BackEndJobConfigurationActor : UntypedActor {
        protected override void OnReceive(object message) {
            Console.WriteLine("Here we are");
        }

    }
}
