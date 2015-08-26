
using Akka.Actor;
using BackEndSystem.Common.Messages;
using Serilog;
using Serilog.Context;

namespace BackEndJobs.Actors {
    public class BackEndJobAActor : ReceiveActor {
        public BackEndJobAActor() {
            var parameter = 1;
            Receive<StartBackEndJobMessage>(m => {
                using (LogContext.PushProperty("JobId", parameter)) {
                    Log.Information(m.Id.ToString());
                }
            });
        }
    }
}
