using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentRequest
    {
        //public Agent agent { get; set; }
        //public Bank bank { get; set; }
        //public Contact contact { get; set; }
        //public Moa moa { get; set; }
        public string User { get; set; }
        public string MasterSubID { get; set; }
        public string Masteridlist { get; set; }
        public string ApplicationID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string IsCorpName { get; set; }
        public string CorpName { get; set; }
        public string IsMerchcategory { get; set; }
        public string Merchcategory { get; set; }
        public string IsBusinessName { get; set; }
        public string BusinessName { get; set; }
        public int DailyDepLimit { get; set; }
        public string PhoneNo { get; set; }
        public string StreetNo { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Comptin { get; set; }
        public string CTCNo { get; set; }
        public MoaRequest[] Auth { get; set; }
        public AgentBranchesRequest[] AgentBranches { get; set; }
        public TerminalRequest[] Terminal { get; set; }
        public string CLastName { get; set; }
        public string CFirstName { get; set; }
        public string CMiddleName { get; set; }
        public string CDesignation { get; set; }
        public string CDepartment { get; set; }
        public string CContactno { get; set; }
        public string CFaxno { get; set; }
        public string CEmailAdd { get; set; }
        public string CBillLName { get; set; }
        public string CBillFName { get; set; }
        public string CBillMName { get; set; }
        public string CBillContactNo { get; set; }
        public string DepBank { get; set; }
        public string BStreetNo { get; set; }
        public string BTown { get; set; }
        public string BCity { get; set; }
        public string BCountry { get; set; }
        public string BPostalCode { get; set; }
        public string Bankaccname { get; set; }
        public string RBOType { get; set; }
        public string RBOLastName { get; set; }
        public string RBOFirstName { get; set; }
        public string RBOMiddleName { get; set; }
        public string RBOEmailAdd { get; set; }
        public string RBOContactNo { get; set; }
        public BankFeesRequest[] BankFeesList { get; set; }
    }
}
