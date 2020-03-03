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
    public class AccountRepository : IAccountRepository
    {
        private readonly ABMSContext _context;
        public AccountRepository(ABMSContext context)
        {
            _context = context;
        }

        public async Task<Tuple<bool, UserAccounts>> GetUser(Login login)
        {
            bool result = await _context.UserInformation.AnyAsync(x => x.Username == login.Username && x.Password == login.Password);
            var userAccounts = await _context.UserInformation.Where(x => x.Username == login.Username && x.Password == login.Password).Select(x => x).FirstOrDefaultAsync();
            return Tuple.Create(result, userAccounts);
        }

        public async Task<ActionResult<IEnumerable<UserAccounts>>> GetAllUsers()
        {
            List<UserAccounts> userAccounts = new List<UserAccounts>();
            userAccounts = await _context.UserInformation.FromSql("dbo.SP_UserInformationReadAll").ToListAsync();
            return userAccounts;
        }

        public async Task<UserAccounts> RegisterUser(UserAccounts userAccounts)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserInformationCreate " +
                     "@id = {0}, @email = {1}, @fullname = {2}, @isactive = {3}, @ischanged  = {4}, @isdeleted = {5}, @password = {6}, @roleid = {7}, " +
                     "@username = {8}",
                      userAccounts.Id, userAccounts.Email, userAccounts.FullName, userAccounts.IsActive, userAccounts.IsChanged, userAccounts.IsDeleted,
                      userAccounts.Password, userAccounts.RoleID, userAccounts.Username);

            }
            catch (Exception ex)
            {
                throw;
            }

            return userAccounts;
        }

        public async Task<string> GetUserDetails(int id)
        {
            var pass = await _context.UserInformation.Where(x => x.Id == id).Select(x => x.Password).SingleOrDefaultAsync();
            return pass;
        }

        public async Task<UserAccounts> UpdateUser(UserAccounts userAccounts)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserInformationUpdate " +
                     "@id = {0}, @roleid = {1}, @username = {2}, @password = {3}, @fullname  = {4}, @email = {5}, @isactive = {6}, @ischanged = {7}, " +
                     "@isdeleted = {8}",
                      userAccounts.Id, userAccounts.RoleID, userAccounts.Username, userAccounts.Password, userAccounts.FullName, userAccounts.Email, userAccounts.IsActive, userAccounts.IsChanged,
                      userAccounts.IsDeleted);

            }
            catch (Exception ex)
            {
                throw;
            }

            return userAccounts;
        }

        public async Task<int> DeleteUser(int userAccountsID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserInformationDelete @id = {0}", userAccountsID);
            }
            catch (Exception ex)
            {
                throw;
            }
            return userAccountsID;
        }

        public async Task<bool> CheckExistingUser(UserAccounts userAccounts)
        {
            bool result = await _context.UserInformation.AnyAsync(x => x.Username == userAccounts.Username);
            return result;
        }

        public async Task<bool> CheckExistingEmail(string email)
        {
            bool result = await _context.UserInformation.AnyAsync(x => x.Email == email && x.IsActive == true);
            return result;
        }

        public async Task<UserAccounts> ChangePasswordAccount(UserAccounts userAccounts)
        {
            var data = _context.Set<UserAccounts>().Local.FirstOrDefault(f => f.Id == userAccounts.Id);
            _context.Entry(userAccounts).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userAccounts;
        }

        public async Task<Tuple<string, string, string, int, bool, bool, int>> GetUserDetailsForPasswordReset(ChangePass email)
        {
            var emailAdd = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.Email).SingleOrDefaultAsync();
            var fullname = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.FullName).SingleOrDefaultAsync();
            var username = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.Username).SingleOrDefaultAsync();
            var roles = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.RoleID).SingleOrDefaultAsync();
            var isActive = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.IsActive).SingleOrDefaultAsync();
            var isChanged = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.IsChanged).SingleOrDefaultAsync();
            int id = await _context.UserInformation.Where(x => x.Email == email.Email).Select(x => x.Id).SingleOrDefaultAsync();
            return Tuple.Create(emailAdd, fullname, username, roles, isActive, isChanged, id);
        }
    }
}
