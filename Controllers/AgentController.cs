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
        //[HttpPost("UpdateAgent")]
        //public async Task<AgentRequest> UpdateAgent([FromBody] AgentRequest agentRequest)
        //{
        //    var agentResponse = await _agentService.UpdateAgent(agentRequest);
        //    return agentResponse;
        //}

        ////Get all Merchants
        //[HttpGet("GetAgents")]
        //public async Task<ActionResult<IEnumerable<AgentRequest>>> GetAgents()
        //{
        //    var agentResponse = await _agentRepository.GetAgents();
        //    return agentResponse;
        //}

        ////Delete Merchants
        //[HttpPost("DeleteAgent")]
        //public async Task<AgentRequest> DeleteMerchant([FromBody] int agentRequest)
        //{
        //    var agentResponse = await _agentService.DeleteAgent(agentRequest);
        //    return agentResponse;
        //}
    }
}