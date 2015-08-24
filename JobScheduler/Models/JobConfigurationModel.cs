using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler.Models {
    public class JobConfigurationModel {
        public JobConfigurationModel() {

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTimeOffset ScheduledTime { get; set; }
    }
}
