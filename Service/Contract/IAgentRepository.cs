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
        Task<Agent> AddAgent(Agent agent);
        Task<Agent> UpdateAgent(Agent agent);
        Task<Agent> DeleteAgent(Agent agent);
        Task<Agent> GetAgent(int id);
    }
}
