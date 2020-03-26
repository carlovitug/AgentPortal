using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentBranchesRequest
    {
        public string ABranchName { get; set; }
        public string AStreetNo { get; set; }
        public string ATown { get; set; }
        public string ACity { get; set; }
        public string Acountry { get; set; }
        public string APostalCode { get; set; }
        public string APhoneNo { get; set; }
    }
}
