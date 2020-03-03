using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class ChangePass
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
