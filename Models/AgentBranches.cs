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
        public string AgentBranchName { get; set; }
        public string StreetNo { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string PhoneNo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
