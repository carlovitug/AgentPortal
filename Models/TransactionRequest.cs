using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class TransactionRequest
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}