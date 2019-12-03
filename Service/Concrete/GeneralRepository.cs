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
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ABMSContext _context;
        public GeneralRepository(ABMSContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<General>>> GetGenerals()
        {
            return await _context.General.ToListAsync();
        }

        public async Task<General> GetGeneral(int id)
        {
            return await _context.General.Where(w => w.ID == id).FirstOrDefaultAsync();
        }

        public async Task<General> UpdateGeneral(General agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<General> AddGeneral(General agent)
        {
            await _context.General.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<General> DeleteGeneral(General agent)
        {
            _context.Entry(agent).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return agent;
        }
    }
}
