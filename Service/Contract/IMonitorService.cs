using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;

namespace ABMS_Backend.Service.Contract
{
    public interface IMonitorService
    {
        Task<List<FinancialMessage>> GetFinancialMessages(FinancialMessageRequest req);
        Task<List<FinancialMessage>> GetCurrentFinancialMessages(FinancialMessageRequest req);
    }
}
