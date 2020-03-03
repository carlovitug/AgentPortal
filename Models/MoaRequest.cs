using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class MoaRequest
    {
        public string AuthFirstName { get; set; }
        public string AuthMiddleName { get; set; }
        public string AuthLastName { get; set; }
        public string AuthDesignation { get; set; }
        public string ValidIDType { get; set; }
        public string ValidIDNumber { get; set; }
        public string ValidIDExpdate { get; set; }
    }
}
