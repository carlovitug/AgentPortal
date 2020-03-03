using ABMS_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAgentService
    {
        Task<AgentRequest> CreateAgent(AgentRequest agentRequest);
        Task<AgentRequest> UpdateAgent(AgentRequest agentRequest);
        //Task<AgentRequest> DeleteAgent(int agentRequest);
    }
}
