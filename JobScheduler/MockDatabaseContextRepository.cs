using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler {
    public class MockDatabaseContextRepository : IDatabaseContextRepository {
        public IList<string> GetJobConfigurations() {
            throw new NotImplementedException();
        }
    }
}
