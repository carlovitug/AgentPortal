using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentList
    {
        public List<Agent> Agent { get; set; }
        public List<Bank> Bank { get; set; }
        public List<AgentBranches> AgentBranches { get; set; }
        public List<BankFees> BankFees { get; set; }        
        public List<Contact> Contact { get; set; }
        public List<Moa> Moa { get; set; }
        public List<Terminal> Terminal { get; set; }
    }
}
