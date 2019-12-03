using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class General
    {
        public int ID { get; set; }
        public decimal Convenience_Fee { get; set; }
        public decimal Deposit_Fee { get; set; }
        public decimal Withdrawal_Fee { get; set; }
        public decimal Balance_Inquiry_Fee { get; set; }
    }
}
