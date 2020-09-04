using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Total_Transaction_Across_Terminal_Per_Day_Per_Acquirer
    {
        public string REP_ID { get; set; }
        public string Branch { get; set; }
        public string SequenceNo { get; set; }
        public string DateTime { get; set; }
        public string CardNo { get; set; }
        public string RRNo { get; set; }
        public string SystemTraceNo { get; set; }
        public string ARN_NO { get; set; }
        public string Bill_Amount { get; set; }
        public string Bill_Ccy { get; set; }
        public string TxnAmount { get; set; }
        public string Txn_Ccy { get; set; }
        public string SettlementAmount { get; set; }
        public string Settlement_Ccy { get; set; }
        public string Fee { get; set; }
        public string TerminalId { get; set; }
        public string CountryCode { get; set; }
    }
}