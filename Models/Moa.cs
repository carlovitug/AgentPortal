using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class Moa
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int ApplicationID { get; set; }
        public int AuthID { get; set; }
        public string AuthFirstName { get; set; }
        public string AuthMiddleName { get; set; }
        public string AuthLastName { get; set; }
        public string AuthDesignation { get; set; }
        public string ValidIDType { get; set; }
        public string ValidIDNumber { get; set; }
        public string ValidIDExpdate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDeteTime { get; set; }
        public string UserCreate { get; set; }
        public string LastUserUpdate { get; set; }

    }
}
