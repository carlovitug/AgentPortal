using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class MasterAgentID
    {
        [Key]
        public int MasterAgentCodeID { get; set; }
    }
}
