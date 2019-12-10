using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            List<Branch> branches = new List<Branch>();
            try
            {
                branches = await _context.Branch.FromSql("dbo.SP_BranchCRUD @StatementType = {0}", "RA").ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return branches;
        }

        public async Task<Branch> GetBranch(int id)
        {
            Branch branch = new Branch();
            try
            {
                branch = await _context.Branch.FromSql("dbo.SP_BranchCRUD @StatementType = {0}, @id = {1}", "R", id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return branch;
        }

        public async Task<Branch> AddBranch(Branch branch)
        {
            try
            {
                
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BranchCRUD " +
                    "@name = {0}, @location  = {1}, @telephone = {2}, @email = {3}, @user = {4}, @StatementType = {5}", 
                    branch.Name, branch.Location, branch.Telephone, branch.Email, branch.User, "C");
            }
            catch (Exception)
            {
                throw;
            }
            return branch;

        }

        public async Task<Branch> UpdateBranch(Branch branch)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BranchCRUD " +
                     "@name = {0}, @location  = {1}, @telephone = {2}, @email = {3}, @user = {4}, @StatementType = {5}, @id = {6}",
                     branch.Name, branch.Location, branch.Telephone, branch.Email, branch.User, "U", branch.ID);
            }
            catch (Exception ex)
            {
                var temp = ex.Message;
                throw;
            }
            
            return branch;
        }

        public async Task<Branch> DeleteBranch(Branch branch)
        {
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BranchCRUD " +
                     "@StatementType = {0}, @id = {1}", "D", branch.ID);
            return branch;
        }
    }
}
