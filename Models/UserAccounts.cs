using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class UserAccounts
    {
        [Key]
        public int Id { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }
    }
}
