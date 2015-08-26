using Akka.Actor;
using BackEndSystem.Common.Messages;
using Serilog;

namespace BackEndJobs.Actors {
    public class BackEndJobAActor : ReceiveActor {
        public BackEndJobAActor() {
            Receive<StartBackEndJobMessage>(m => {
                Log.Information(m.Id.ToString());
            });
        }
    }
}
