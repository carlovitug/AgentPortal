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

        //Returns list of agents
        [HttpGet("GetAgents")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            return await _agentRepository.GetAgents();
        }

        //Returns an agent
        [HttpGet("GetAgent")]
        public async Task<IActionResult> GetAgent(int id)
        {
            if(id > 0)
            {
                var temp = await _agentRepository.GetAgent(id);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }

        //Add new agent record
        [HttpPost("AddAgent")]
        public async Task<ActionResult<Agent>> AddAgent(Agent agent)
        {
            if(agent.ID > 0)
            {
                var temp = await _agentRepository.AddAgent(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
            
        }

        //Updates agent record
        [HttpPost("UpdateAgent")]
        public async Task<ActionResult<Agent>> UpdateAgent(Agent agent)
        {
            
            if(agent.ID > 0)
            {
                var temp = await _agentRepository.UpdateAgent(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }

        //Delete agent record
        [HttpGet("DeleteAgent")]
        public async Task<ActionResult<Agent>> DeleteAgent(int id)
        {
            if (id > 0)
            {
                var temp = await _agentRepository.DeleteAgent(new Agent { ID = id });
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }
    }
}