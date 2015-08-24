using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class BackendJobConfigurationActor : UntypedActor {
        IDatabaseContextRepository dbContext;
        public BackendJobConfigurationActor(IDatabaseContextRepository dbContext) {
            this.dbContext = dbContext;
        }

        protected override void OnReceive(object message) {
            dbContext.GetJobConfigurationsAsync().Wait();//TODO : remove later
            
            Console.WriteLine("Here we are");
        }

    }
}
