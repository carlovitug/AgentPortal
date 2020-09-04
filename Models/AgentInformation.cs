using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentInformation
    {

        public int ID { get; set; }
        public string RequestID { get; set; }
        public int MasterAgentCodeID { get; set; }
        public int SubAgentCodeID { get; set; }
        public int AgentID { get; set; }
        public bool IsCorporate { get; set; }
        public string CorporateName { get; set; }
        public bool IsMerchCategory { get; set; }
        public string MerchantCategory { get; set; }
        public bool IsBusiness { get; set; }
        public string BusinessName { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string StreetNo { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string CompanyTIN { get; set; }
        public int CTCNo { get; set; }
        public int DailyDepositLimit { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
