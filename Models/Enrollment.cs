using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int AgentID { get; set; }
        public int PostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime InsertDT { get; set; }
        public DateTime UpdateDT { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Nationality { get; set; }
        public string ReferenceNo { get; set; }
        public string Doc1Type { get; set; }
        public string Doc1 { get; set; }
        public string Doc2Type { get; set; }
        public string Doc2 { get; set; }
        public string Doc3Type { get; set; }
        public string Doc3 { get; set; }

    }
}