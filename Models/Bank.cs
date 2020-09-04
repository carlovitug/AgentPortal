using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Bank
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public string DepositoryBank { get; set; }
        public string StreetNo { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string BankAccountName { get; set; }
        public string RBOType { get; set; }
        public string RBOFName { get; set; }
        public string RBOMName { get; set; }
        public string RBOLName { get; set; }
        public string RBOEmailAdd { get; set; }
        public string RBOContactNo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
