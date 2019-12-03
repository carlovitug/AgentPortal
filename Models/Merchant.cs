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
        public int  Merchant_ID { get; set; }
        public string Merchant_Name { get; set; }
        public string Main_Contact { get; set; }
        public string Location { get; set; }
        public string Telephone { get; set; }
        public  string Email { get; set; }

    }
}
