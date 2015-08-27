using Akka.Actor;
using BackEndSystem.Common;
using BackEndSystem.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Actors {
    public class EmptyRemoteJobActor : LogEnabledRecieveActor {
        public EmptyRemoteJobActor() {
        }

    }
}
