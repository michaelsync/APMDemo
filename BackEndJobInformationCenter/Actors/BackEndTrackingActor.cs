
using Akka.Actor;
using BackEndSystem.Common;
using BackEndSystem.Common.Messages;
using Serilog;
using Serilog.Context;
using System.Collections.Concurrent;

namespace BackEndJobInformationCenter.Actors {
    public class BackEndTrackingActor : LogEnabledRecieveActor {
        private ConcurrentDictionary<int, string> JobDictionary = new ConcurrentDictionary<int, string>(); 
        public BackEndTrackingActor() {

            Receive<StartBackEndJobMessage>(m => {
                JobDictionary.TryAdd(m.Id, m.Name);

                Log.Information("Job Count : {0}", JobDictionary.Count);
            });
        }
    }
}
