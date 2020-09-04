using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Daily_Atm_Recon
    {
        public string TerminalNumber { get; set; }
        public string SequenceNumber { get; set; }
        public string Stan { get; set; }
        public string Txn_DateTime { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public string Response { get; set; }
        public string Remarks { get; set; }
        public string Onus_Acq { get; set; }
    }
}