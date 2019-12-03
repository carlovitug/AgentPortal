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
    public class MerchantRepository : IMerchantRepository
    {
        private readonly ABMSContext _context;
        public MerchantRepository(ABMSContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Merchant>>> GetMerchants()
        {
            try
            {
                return await _context.Merchant.ToListAsync();
            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            
        }

        public async Task<Merchant> GetMerchant(int id)
        {
            return await _context.Merchant.Where(w => w.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Merchant> UpdateMerchant(Merchant agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Merchant> AddMerchant(Merchant agent)
        {
            await _context.Merchant.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent;
        }

        public async Task<Merchant> DeleteMerchant(Merchant agent)
        {
            _context.Entry(agent).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return agent;
        }
    }
}
