using ABMS_Backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionData>> GetTransaction(string dateFrom, string dateTo);
        Task<IEnumerable<TransactionData>> GetTransaction();
        Task<IEnumerable<TransactionData>> GetAgentTransaction(string dateFrom, string dateTo, int agentID);
        Task<IEnumerable<TransactionData>> GetAgentTransaction(int agentID);
    }
}