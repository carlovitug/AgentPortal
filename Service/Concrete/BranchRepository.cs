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
    public class BranchRepository : IBranchRepository
    {
        private readonly ABMSContext _context;
        public BranchRepository(ABMSContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            return await _context.Branch.ToListAsync();
        }

        public async Task<Branch> GetBranch(int id)
        {
            return await _context.Branch.Where(w => w.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Branch> UpdateBranch(Branch agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Branch> AddBranch(Branch agent)
        {
            try
            {
                await _context.Branch.AddAsync(agent);
                await _context.SaveChangesAsync();
                return agent;
            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
          
        }

        public async Task<Branch> DeleteBranch(Branch agent)
        {
            _context.Entry(agent).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return agent;
        }
    }
}
