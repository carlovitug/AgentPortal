using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Merchant
    {
        
        public int ID { get; set; }
        public int  BranchID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Geolocation { get; set; }
        public bool Deactivated { get; set; }
        public bool Enrolment { get; set; }
        public bool Withdrawal { get; set; }
        public bool Purchase { get; set; }
        public bool Inquiry { get; set; }
        public string User { get; set; }

    }
}
