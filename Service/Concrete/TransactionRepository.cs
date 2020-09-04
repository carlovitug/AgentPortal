using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ABMSContext _context;
        public TransactionRepository(ABMSContext context)
        {
            _context = context;
        }

        #region Admin
        public async Task<IEnumerable<TransactionData>> GetTransactionData(string dateFrom, string dateTo)
        {
            //var temp = await _context.TransactionData
            //    .Where(w => Convert.ToDateTime(w.InsertDT) >= dateFrom && Convert.ToDateTime(w.InsertDT) <= dateTo).ToListAsync();
            var temp = await _context.TransactionData.FromSql("dbo.SP_GetTransactionData " +
                    "@datefrom = {0}, @dateto = {1} ", dateFrom, dateTo).ToListAsync();
            return temp;
        }

        #endregion

        #region Agent
        public async Task<IEnumerable<TransactionData>> GetAgentTransactionData(string dateFrom, string dateTo, int agentID)
        {
            var temp = await _context.TransactionData.FromSql("dbo.SP_GetAgentTransactionData " +
                    "@datefrom = {0}, @dateto = {1}, @agentid = {2}", dateFrom, dateTo, agentID).ToListAsync();
            return temp;
        }
        #endregion

    }
}