using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;

namespace ABMS_Backend.Service.Concrete
{
    public class MonitorRepository : IMonitorRepository
    {
        ABMSContext _context;
        public MonitorRepository(ABMSContext aBMSContext)
        {
            _context = aBMSContext;
        }
    }
}
