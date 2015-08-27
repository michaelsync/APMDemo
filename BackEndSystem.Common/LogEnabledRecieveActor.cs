using Akka.Actor;
using Akka.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSystem.Common {
    //Why?
    //Any plans to automatically collect actor lifecycle and message received data?
    //That depends largely on how much traction Akka.Monitoring gets -we're considering subclassing UntypedActor and TypedActor to automatically provide lifecycle and receive instrumentation, but we want to see how other people use it first.
    //https://github.com/Aaronontheweb/akka-monitoring

    public class LogEnabledRecieveActor : ReceiveActor{
        protected override void PreStart() {
            Context.IncrementActorCreated(); //Need to add enable/disable logic
        }

        protected override void PostStop() {
            Context.IncrementActorStopped(); //Need to add enable/disable logic
        }
    }
}
