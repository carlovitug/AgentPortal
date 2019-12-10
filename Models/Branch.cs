using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Branch
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public  string Telephone { get; set; }
        public string Email { get; set; }
        public string User { get; set; }


    }
}
