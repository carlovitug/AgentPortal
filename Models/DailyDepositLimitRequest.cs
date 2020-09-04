using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class DailyDepositLimitRequest
    {
        public string RequestID { get; set; }
        public int DailyDepLimit { get; set; }
    }
}
