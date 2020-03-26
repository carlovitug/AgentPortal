using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class RequestIDModel
    {
        [Key]
        public string RequestID { get; set; }
    }
}
