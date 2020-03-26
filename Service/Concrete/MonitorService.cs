using ABMS_Backend.Service.Contract;
using ABMS_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ABMS_Backend.DataAccess;

namespace ABMS_Backend.Service.Concrete
{
    public class MonitorService : IMonitorService
    {
        private readonly ABMSContext _context;
        public MonitorService(ABMSContext aBMSContext)
        {
            _context = aBMSContext;
        }

        //Get List of Financial Message on a filtered date
        public async Task<List<FinancialMessage>> GetFinancialMessages(FinancialMessageRequest req)
        {
            var temp = new List<FinancialMessage>();
            await Task.Run(() => {
                using (var da = new MonitorDataAccess()
                {
                    query = Query.getfinancialmessage,
                    sqlParameters = new SqlParameter[] {
                        new SqlParameter("@datefrom",req.DateFrom),
                         new SqlParameter("@dateto",req.DateTo)
                    }

                })
                {
                    temp = da.GetFinacialMessages();
                }
            });
            return temp;
        }

        //Get List of Financial Message within this day.
        public async Task<List<FinancialMessage>> GetCurrentFinancialMessages(FinancialMessageRequest req)
        {
            var temp = new List<FinancialMessage>();
            await Task.Run(() => {
                using (var da = new MonitorDataAccess()
                {
                    query = Query.getcurrentfinancialmessage,
                    sqlParameters = new SqlParameter[] {
                        new SqlParameter("@datefrom",req.DateFrom)
                    }

                })
                {
                    temp = da.GetFinacialMessages();
                }
            });
            return temp;
        }
    }
}
