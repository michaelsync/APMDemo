using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSystem.Common.Messages {
    public class StartBackEndJobMessage : IConsistentHashable {
        public StartBackEndJobMessage(int id) {
            this.Id = id;
        }
        public int Id { get; private set; }
        public string Name { get; set; }
        public object ConsistentHashKey {
            get {
                return this.GetHashCode();
            }
        }
    }
}
