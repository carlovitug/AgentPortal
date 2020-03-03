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
        Task<ActionResult<IEnumerable<Agent>>> GetAgents();
        Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents();
        Task<ActionResult<IEnumerable<Agent>>> GetSubAgents([FromBody] int agentRequestID);
        Task<ActionResult<IEnumerable<Agent>>> GetMasterAgentID([FromBody] int agentRequestID);
        Task<Tuple<Agent, Bank, Contact, AgentBranches>> CreateAgent(Agent agent, Bank bank, Contact contact, AgentBranches agentBranches);
        Task<Moa> CreateMoa(Moa moa);
        Task<Terminal> CreateTerminal(Terminal terminal);
        Task<AgentRequest> UpdateAgent(AgentRequest agentRequest);
        Task<int> DeleteAgent(int agentRequestID);
        Task<bool> CheckExistingAgentID(int agentID);
        
    }
}
