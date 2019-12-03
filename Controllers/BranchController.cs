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
    public class BranchController : Controller
    {
        //public IAgentService _agentService;
        public IBranchRepository _agentRepository;
        public BranchController(IBranchRepository branchRepository)
        {
            //_agentService = agentService;
            _agentRepository = branchRepository;
        }
        //Returns list of branch records
        [HttpGet("GetBranches")]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            return await _agentRepository.GetBranches();
        }

        //Returns a branch record
        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch(int id)
        {
            if (id > 0)
            {
                var temp = await _agentRepository.GetBranch(id);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }

        //Add new branch record
        [HttpPost("AddBranch")]
        public async Task<ActionResult<Branch>> AddBranch(Branch agent)
        {
            if (agent.ID > 0)
            {
                var temp = await _agentRepository.AddBranch(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }

        //Update branch record
        [HttpPost("UpdateBranch")]
        public async Task<ActionResult<Branch>> UpdateBranch(Branch agent)
        {
            if(agent.ID > 0)
            {
                var temp = await _agentRepository.UpdateBranch(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
            
        }

        //Delete branch record
        [HttpGet("DeleteBranch")]
        public async Task<ActionResult<Branch>> DeleteBranch(int id)
        {
            if(id > 0)
            {
                var temp = await _agentRepository.DeleteBranch(new Branch { ID = id });
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
                
            
            
            
        }
    }
}