using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAgentService
    {
        Task<AgentRequest> CreateAgent(AgentRequest agentRequest);  
        Task<AgentRequestEdit> UpdateAgent(AgentRequestEdit agentRequestEdit);
        Task<AgentList> GetAgentwithID([FromBody] int id);
        Task<int> CreateAgentID();
        Task<string> CreateRequestID();
        Task<int> CreateMasterSubID();
    }
}
