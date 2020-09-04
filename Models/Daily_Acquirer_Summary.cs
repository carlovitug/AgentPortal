using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Daily_Acquirer_Summary
    {
        public string REP_ID { get; set; }
        public string TerminalNumber { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }
        public string BILL_CCY { get; set; }
    }
}