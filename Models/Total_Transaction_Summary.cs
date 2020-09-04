using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Total_Transaction_Summary
    {
        public string REP_ID { get; set; }
        public string Branch { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }
        public string Bill_Ccy { get; set; }
    }
}