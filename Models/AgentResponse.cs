﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class AgentResponse
    {
        [Key]
        public int ID { get; set; }
        public Agent agent { get; set; }
        public Bank bank { get; set; }
        public Contact  contact { get; set; }
        public Moa moa { get; set; }

    }
}
