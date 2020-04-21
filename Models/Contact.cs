using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string RequestID { get; set; }
        public string ApplicationID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingMiddleName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingContactNo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
