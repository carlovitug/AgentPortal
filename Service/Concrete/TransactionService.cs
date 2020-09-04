using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using ABMS_Backend.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class TransactionService : ITransactionService
    {
        public ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        #region Admin
        public async Task<IEnumerable<TransactionData>> GetTransaction(string dateFrom, string dateTo)
        {
            DateTime dtFrom = DateTime.Parse(dateFrom);
            DateTime dtTo = DateTime.Parse(dateTo);
            var res = await _transactionRepository.GetTransactionData(dateFrom, dateTo);
            return res;
        }

        public async Task<IEnumerable<TransactionData>> GetTransaction()
        {
            string dateToday = DateTime.Now.ToString("yyyy-MM-dd");
            string minTime = "00:00", maxTime = "23:59";
            string min = dateToday + ' ' + minTime;
            string max = dateToday + ' ' + maxTime;
            DateTime dateMin = DateTime.Parse(min), dateMax = DateTime.Parse(max);
            var res = await _transactionRepository.GetTransactionData(dateToday, dateToday);
            return res;
        }
        #endregion

        #region Agent
        public async Task<IEnumerable<TransactionData>> GetAgentTransaction(string dateFrom, string dateTo, int agentID)
        {
            DateTime dtFrom = DateTime.Parse(dateFrom);
            DateTime dtTo = DateTime.Parse(dateTo);
            var res = await _transactionRepository.GetAgentTransactionData(dateFrom, dateTo, agentID);
            return res;
        }
        
        public async Task<IEnumerable<TransactionData>> GetAgentTransaction(int agentID)
        {
            string dateToday = DateTime.Now.ToString("yyyy-MM-dd");
            string minTime = "00:00", maxTime = "23:59";
            string min = dateToday + ' ' + minTime;
            string max = dateToday + ' ' + maxTime;
            DateTime dateMin = DateTime.Parse(min), dateMax = DateTime.Parse(max);
            var res = await _transactionRepository.GetAgentTransactionData(dateToday, dateToday, agentID);
            return res;
        }
        #endregion
    }
}