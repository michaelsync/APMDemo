using Akka.Actor;
using JobManager.Messages;
using JobManager.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Actors {
    public class DatabaseConfigurationActor : ReceiveActor {
        public DatabaseConfigurationActor() {

            Receive<GetAllJobConfigurationsFromDbMessage>(m => {
                Log.Information("Received GetAllJobConfigurationsFromDbMessage.");

                var allJobConfigs = new List<JobConfigurationModel> {
                    new JobConfigurationModel() { Id =1, Name = "BackEndJobA" },
                    new JobConfigurationModel() { Id =2, Name = "BackEndJobB" }
                };

                Context.Sender.Tell(allJobConfigs);
            });
        }

        
     }
}
