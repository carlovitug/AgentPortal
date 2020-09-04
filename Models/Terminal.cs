using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Terminal
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public string POSTerminalName { get; set; }
        public string TypeOfPOSTerminal { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }

    }
}
