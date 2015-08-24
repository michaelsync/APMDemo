using Akka.Actor;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendJobA.Actors {
    public class BackEndJobAActor : ReceiveActor {
        public BackEndJobAActor() {
            Receive<string>(m => {
                Log.Information(m);
            });
        }
    }
}
