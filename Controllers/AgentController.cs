using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : Controller
    {
        public IAgentService _agentService;
        public IAgentRepository _agentRepository;
        public AgentController(IAgentService agentService, IAgentRepository agentRepository)
        {
            _agentService = agentService;
            _agentRepository = agentRepository;
        }

        [HttpPost("CreateAgent")]
        public async Task<AgentRequest> CreateAgent([FromBody] AgentRequest agentRequest)
        {
            var agentResponse = await _agentService.CreateAgent(agentRequest);
            return agentRequest;    
        }

        //Update Merchants
        [HttpPost("UpdateAgent")]
        public async Task<AgentRequestEdit> UpdateAgent([FromBody] AgentRequestEdit agentRequestEdit)
        {
            var agentResponse = await _agentService.UpdateAgent(agentRequestEdit);
            return agentResponse;
        }

        [HttpPost("ChangeStatus")]
        public async Task<ChangeStatus> ChangeStatus(ChangeStatus status)
        {
            var agentResponse = await _agentRepository.ChangeStatus(status);
            return agentResponse;
        }

        ////Get all Merchants
        [HttpGet("GetAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            var agentResponse = await _agentRepository.GetAgents();
            return agentResponse;
        }

        [HttpGet("GetPendingAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetPendingAgents()
        {
            var agentResponse = await _agentRepository.GetPendingAgents();
            return agentResponse;
        }

        ////Get all Merchants
        [HttpPost("GetAgentwithID")]
        public async Task<AgentList> GetAgentwithID([FromBody] int id)
        {
            var agentResponse = await _agentService.GetAgentwithID(id);
            return agentResponse;
        }

        [HttpGet("GetMasterAgents")]
        public async Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents()
        {
            var agentResponse = await _agentRepository.GetMasterAgents();
            return agentResponse;
        }

        [HttpPost("GetSubAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetSubAgents([FromBody] int agentRequestID)
        {
            var agentResponse = await _agentRepository.GetSubAgents(agentRequestID);
            return agentResponse;
        }

        [HttpPost("GetMGR")]
        public async Task<ActionResult<IEnumerable<BankFees>>> GetMGR([FromBody] MGRRequest mgrRequest)
        {
            var agentResponse = await _agentRepository.GetMGR(mgrRequest);
            return agentResponse;
        }
        [HttpPost("GetMasterAgentID")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetMasterAgentID([FromBody] int agentRequestID)
        {
            var agentResponse = await _agentRepository.GetMasterAgentID(agentRequestID);
            return agentResponse;
        }

        [HttpPost("UpgradeAgent")]
        public async Task<bool> UpgradeAgent([FromBody] DailyDepositLimitRequest dailyDepositLimitRequest)
        {
            var agentResponse = await _agentRepository.UpgradeDailyDepositLimit(dailyDepositLimitRequest);
            return agentResponse;
        }

        [HttpPost("UpdateMGR")]
        public async Task<bool> UpdateMGR([FromBody] MGRRequest mgrRequest)
        {
            var agentResponse = await _agentRepository.UpdateMGR(mgrRequest);
            return agentResponse;
        }

        [HttpPost("UpdatePhone")]
        public async Task<bool> UpdatePhone([FromBody] PhoneRequest phoneRequest)
        {
            var agentResponse = await _agentRepository.UpdatePhone(phoneRequest);
            return agentResponse;
        }

        //Delete Merchants
        [HttpPost("DeleteAgent")]
        public async Task<int> DeleteMerchant([FromBody] int agentRequestID)
        {
            var agentResponse = await _agentRepository.DeleteAgent(agentRequestID);
            return agentResponse;
        }
    }
}