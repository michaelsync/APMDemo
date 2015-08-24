using JobScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler.Messages {
    public class JobConfigurationsMessage {
        public JobConfigurationsMessage(IEnumerable<JobConfigurationModel> items) {
            JobConfigurationModels = new List<JobConfigurationModel>(items);
        }
        public IList<JobConfigurationModel> JobConfigurationModels { get; private set; }
    }
}
