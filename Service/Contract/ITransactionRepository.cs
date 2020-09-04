using ABMS_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionData>> GetTransactionData(string dateFrom, string dateTo);
        Task<IEnumerable<TransactionData>> GetAgentTransactionData(string dateFrom, string dateTo, int agentID);
    }
}