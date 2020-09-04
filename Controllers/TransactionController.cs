using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public ITransactionService _transactionService;
        public ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionService transactionService, ITransactionRepository transactionRepository)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
        }

        #region Admin
        [HttpPost("GetTransactionData")]
        public async Task<IEnumerable<TransactionData>> GetTransactionData(TransactionRequest request)
        {
            var res = await _transactionService.GetTransaction(request.DateFrom, request.DateTo);
            return res;
        }

        [HttpGet("GetTransactionDataToday")]
        public async Task<IEnumerable<TransactionData>> GetTransactionDataToday()
        {
            var res = await _transactionService.GetTransaction();
            return res;
        }
        #endregion

        #region Agent
        [HttpPost("GetAgentTransactionData")]
        public async Task<IEnumerable<TransactionData>> GetAgentTransactionData(AgentTransactionRequest agentTransactionRequest)
        {
            var res = await _transactionService.GetAgentTransaction(agentTransactionRequest.DateFrom, agentTransactionRequest.DateTo, agentTransactionRequest.AgentID);
            return res;
        }

        [HttpPost("GetAgentTransactionDataToday")]
        public async Task<IEnumerable<TransactionData>> GetAgentTransactionDataToday([FromBody]int agentID)
        {
            var res = await _transactionService.GetAgentTransaction(agentID);
            return res;
        }
        #endregion
    }
}