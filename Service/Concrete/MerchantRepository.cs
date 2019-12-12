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
                     "@bid = {0}, @name = {1}, @address = {2}, @geolocation  = {3}, @deactivate = {4}, @enrolment = {5}, @withdrawal = {6}, " +
                     "@purchase = {7}, @inquiry = {8}, @user = {9}, @StatementType = {10}, @id = {11}",
                      merchant.BranchID, merchant.Name, merchant.Address, merchant.Geolocation, merchant.Deactivated, merchant.Enrolment, 
                      merchant.Withdrawal, merchant.Purchase, merchant.Inquiry, merchant.User, "U", merchant.ID);
            }
            catch (Exception)
            {
                throw;
            }
            
            return merchant;
        }

        public async Task<Merchant> AddMerchant(Merchant merchant)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MerchantCRUD " +
                 "@bid = {0}, @name = {1}, @address = {2}, @geolocation  = {3}, @deactivate = {4}, @enrolment = {5}, " +
                 "@withdrawal = {6}, @purchase = {7}, @inquiry = {8}, @user = {9}, @StatementType = {10}, @id = {11}",
                  merchant.BranchID, merchant.Name, merchant.Address, merchant.Geolocation, merchant.Deactivated, 
                  merchant.Enrolment, merchant.Withdrawal, merchant.Purchase, merchant.Inquiry, merchant.User, "C", merchant.ID);
            }
            catch (Exception)
            {
                throw;
            }
            return merchant;
        }

        public async Task<Merchant> DeleteMerchant(Merchant merchant)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MerchantCRUD " +
                     "@StatementType = {0}, @id = {1}", "D", merchant.ID);
            }
            catch (Exception)
            {

                throw;
            }
            return merchant;
        }
    }
}
