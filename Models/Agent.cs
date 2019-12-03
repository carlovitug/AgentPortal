using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Agent
    {
        
        public int ID { get; set; }
        public int Agent_ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}
