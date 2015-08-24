using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class MockDatabaseContextRepository : IDatabaseContextRepository {
        public async Task<IList<string>> GetJobConfigurationsAsync() {
            await Task.Delay(100);
            return new List<string> {
                "JobA",
                "JobB"
            };
        }
    }
}
