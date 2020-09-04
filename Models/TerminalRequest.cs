using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class TerminalRequest
    {
        public string TerminalName { get; set; }
        public string TerminalType { get; set; }
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
    }
}
