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
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentCRUD @StatementType = {0}", "RA").ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return agent;
        }

        public async Task<Agent> GetAgent(int id)
        {
            Agent agent = new Agent();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentCRUD @StatementType = {0}, @id = {1}", "R", id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return agent;
        }

        public async Task<Agent> UpdateAgent(Agent agent)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentCRUD " +
                     "@id = {0}, @mid = {1}, @otp = {2}, @deactivate  = {3}, @fname = {4}, @mname = {5}, @lname = {6}, " +
                     "@gender = {7}, @dob = {8}, @pnum = {9}, @address = {10}, @city = {11}, @state = {12}, @pcode = {13},"+
                     "@nationality = {14}, @user = {15}, @StatementType = {16}",
                      agent.ID, agent.MerchantID, agent.OTP, agent.Deactivated, agent.FirstName, agent.MiddleName, agent.LastName,
                      agent.Gender, agent.DateOfBirth, agent.PhoneNum, agent.Address, agent.City, agent.State, agent.PostalCode,
                      agent.Nationality, agent.User, "U");
            }
            catch (Exception)
            {
                throw;
            }

            return agent;
        }

        public async Task<Agent> AddAgent(Agent agent)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentCRUD " +
                 "@id = {0}, @mid = {1}, @otp = {2}, @deactivate  = {3}, @fname = {4}, @mname = {5}, @lname = {6}, " +
                     "@gender = {7}, @dob = {8}, @pnum = {9}, @address = {10}, @city = {11}, @state = {12}, @pcode = {13}," +
                     "@nationality = {14}, @user = {15}, @StatementType = {16}",
                      agent.ID, agent.MerchantID, agent.OTP, agent.Deactivated, agent.FirstName, agent.MiddleName, agent.LastName,
                      agent.Gender, agent.DateOfBirth, agent.PhoneNum, agent.Address, agent.City, agent.State, agent.PostalCode,
                      agent.Nationality, agent.User, "C");
            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            return agent;
        }

        public async Task<Agent> DeleteAgent(Agent agent)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentCRUD " +
                   "@StatementType = {0}, @id = {1}", "D", agent.ID);
            }
            catch (Exception)
            {
                throw;
            }
            return agent;
        }
    }
}
