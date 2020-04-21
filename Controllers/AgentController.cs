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
        [HttpPost("GetAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents([FromBody] int applicationID)
        {
            var agentResponse = await _agentRepository.GetAgents(applicationID);
            return agentResponse;
        }

        [HttpPost("GetPendingAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetPendingAgents([FromBody] int applicationID)
        {
            var agentResponse = await _agentRepository.GetPendingAgents(applicationID);
            return agentResponse;
        }

        ////Get all Merchants
        [HttpPost("GetAgentwithID")]
        public async Task<AgentList> GetAgentwithID([FromBody] int id)
        {
            var agentResponse = await _agentService.GetAgentwithID(id);
            return agentResponse;
        }

        [HttpPost("GetMasterAgents")]
        public async Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents([FromBody] int applicationID)
        {
            var agentResponse = await _agentRepository.GetMasterAgents(applicationID);
            return agentResponse;
        }

        [HttpPost("GetSubAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetSubAgents([FromBody] int agentRequestID)
        {
            var agentResponse = await _agentRepository.GetSubAgents(agentRequestID);
            return agentResponse;
        }

        [HttpPost("GetMasterAgentID")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetMasterAgentID([FromBody] int agentRequestID)
        {
            var agentResponse = await _agentRepository.GetMasterAgentID(agentRequestID);
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