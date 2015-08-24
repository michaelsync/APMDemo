using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public interface IDatabaseContextRepository {

        Task<IList<string>> GetJobConfigurationsAsync();
    }
}
