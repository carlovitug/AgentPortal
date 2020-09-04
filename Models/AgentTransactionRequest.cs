using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentTransactionRequest
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int AgentID { get; set; }
    }
}
