using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentBranches
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public string ApplicationID { get; set; }
        public string NoAgentOutletsOrBranches { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
