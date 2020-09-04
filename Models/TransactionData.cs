using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class TransactionData
    {
        public int Id { get; set; }
        public Nullable<int> AgentID { get; set; }
        public string TransactionType { get; set; }
        //public string PrimaryAccountNumber { get; set; }
        public string AccountType { get; set; }
        public string SettlementAmount { get; set; }
        public string TransactionAmount { get; set; }
        public string SystemTraceAuditNumber { get; set; }
        public string TransactionTime { get; set; }
        //public string TransactionDate { get; set; }
        public string ExpiryDate { get; set; }
        public string POSEntryMode { get; set; }
        //public string CardSequenceNumber { get; set; }
        //public string Track2Data { get; set; }
        //public string CardAcceptorTerminalID { get; set; }
        //public string CardAcceptorID { get; set; }
        public string CardAcceptorName { get; set; }
        //public string CardAcceptorLocation { get; set; }
        //public string PinData { get; set; }
        //public string ChipInformation { get; set; }
        //public string KSN { get; set; }
        //public string ConvenienceFeeAmount { get; set; }
        public string ResponseStatus { get; set; }
        //public string ErrorCode { get; set; }
        //public string MessageToDisplay { get; set; }
        public string ResponseCode { get; set; }
        public string InsertDT { get; set; }
        //public string InsertUser { get; set; }
        //public string UpdateDT { get; set; }
        //public string UpdateUser { get; set; }
        //public string TerminalNo { get; set; }
        //public string SRNO { get; set; }
        public string Trace { get; set; }
        //public string ATM_SEQ { get; set; }
        //public string Retrieval { get; set; }
        //public string CardNumber { get; set; }
        //public string TRANS_DESC { get; set; }
        //public string Account { get; set; }
        //public string Remarks { get; set; }
        //public string SequenceNumber { get; set; }
        //public string Stan { get; set; }
        //public string Onus_Acq { get; set; }
    }
}