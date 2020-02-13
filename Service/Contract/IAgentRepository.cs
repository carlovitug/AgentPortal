using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAgentRepository
    {
        //Task<ActionResult<IEnumerable<AgentRequest>>> GetAgents();
        Task<AgentRequest> CreateAgent(AgentRequest agentRequest);
        //Task<AgentRequest> UpdateAgent(AgentRequest agentRequest);
        //Task<AgentRequest> DeleteAgent(AgentRequest agentRequest);
    }
}
