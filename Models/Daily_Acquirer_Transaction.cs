using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Daily_Acquirer_Transaction
    {
        public string REP_ID { get; set; }
        public string TerminalNumber { get; set; }
        public string SRNO { get; set; }
        public string TRACE { get; set; }
        public string ATM_SEQ { get; set; }
        public string RETRIEVAL { get; set; }
        public string TRX_DATE { get; set; }
        public string TRX_TIME { get; set; }
        public string CARD_NUMBER { get; set; }
        public string TRANS_DESC { get; set; }
        public string ACCOUNT { get; set; }
        public string RESP_CODE { get; set; }
        public string TXN_AMOUNT { get; set; }
        public string AMT_AUTH { get; set; }
        public string REMARKS { get; set; }
    }
}