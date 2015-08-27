
using Akka.Actor;
using BackEndSystem.Common.Messages;
using Serilog;
using Serilog.Context;

namespace BackEndJobInformationCenter.Actors {
    public class BackEndJobAActor : ReceiveActor {

        public BackEndJobAActor(int jobId) {

            Receive<StartBackEndJobMessage>(m => {
                var id = jobId + m.Id;

                using (LogContext.PushProperty("JobId", id)) {
                    Log.Information(id.ToString());

                    Log.Information("Tell Database Reader");

                    Log.Information("Do some updates");
                }
            });
        }
    }
}
