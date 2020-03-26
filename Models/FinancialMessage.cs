using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class FinancialMessage
    {
        public string Insert_DateTime { get; set; }
        public string Merchant { get; set; }
        public string Terminal { get; set; }
        public string BIN { get; set; }
        public string Trx_Type { get; set; }
        public string Message_Type { get; set; }
        public string Message_Mode { get; set; }
        public string Terminal_Type { get; set; }
        public string Entry_Mode { get; set; }
        public string PIN_Mode { get; set; }
        public string Amount { get; set; }//TransactionSettlemenAmount
        public string Response { get; set; }
        public string Code { get; set; } //TransactionErrorCode
        public string STAN { get; set; }//Bancnet Trace/STAN
        public string ResponceCodeText { get; set; }
        public string Serial_No { get; set; }
        public int RefNo { get; set; }
        public string Ori_RefNo { get; set; }
        public string PHID { get; set; }
        public string ConnectorName { get; set; }

        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string MerchantName { get; set; }
        public string IssuingBank { get; set; }
        public string TransactionType { get; set; }
        public string FeeType { get; set; }
        public string FLTMDRAmount { get; set; }
    }
}
