using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ABMSContext _context;
        public AgentRepository(ABMSContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            return await _context.Agent.ToListAsync();
        }

        public async Task<Agent> GetAgent(int id)
        {
            return await _context.Agent.Where(w => w.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Agent> UpdateAgent(Agent agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Agent> AddAgent(Agent agent)
        {
            await _context.Agent.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Agent> DeleteAgent(Agent agent)
        {
            _context.Entry(agent).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return agent;
        }
    }
}
