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
            List<Merchant> merchants = new List<Merchant>();
            try
            {
                merchants = await _context.Merchant.FromSql("dbo.SP_MerchantCRUD @StatementType = {0}", "RA").ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return merchants;
            
        }

        public async Task<Merchant> GetMerchant(int id)
        {
            Merchant merchant = new Merchant();
            try
            {
                merchant = await _context.Merchant.FromSql("dbo.SP_MerchantCRUD @StatementType = {0}, @id = {1}", "R", id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return merchant;
        }

        public async Task<Merchant> UpdateMerchant(Merchant merchant)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MerchantCRUD " +
                     "@id = 0, @bid = {1}, @name = {2}, @geolocation  = {3}, @telephone = {4}, @email = {5}, @user = {6}, @StatementType = {7}",
                     merchant.ID, merchant.BranchID, merchant.Name, merchant.Geolocation, merchant.Telephone, merchant.Email, merchant.User, "U");
            }
            catch (Exception)
            {
                throw;
            }
            
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
