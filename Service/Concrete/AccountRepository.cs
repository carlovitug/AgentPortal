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

        public async Task<bool> LoggedIn(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserLoggedIn " +
                     "@id = {0}", id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> LoggedOut(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserLoggedOut " +
                     "@id = {0}", id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
                     "@username = {8}, @isloggedin = {9}, @agentid = {10}",
                      userAccounts.Id, userAccounts.Email, userAccounts.FullName, userAccounts.IsActive, userAccounts.IsChanged, userAccounts.IsDeleted,
                      userAccounts.Password, userAccounts.RoleID, userAccounts.Username, userAccounts.IsLoggedIn, userAccounts.AgentID);

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
                     "@id = {0}, @roleid = {1}, @username = {2}, @fullname  = {3}, @email = {4}",
                      userAccounts.Id, userAccounts.RoleID, userAccounts.Username, userAccounts.FullName, userAccounts.Email, userAccounts.AgentID);

            }
            catch (Exception ex)
            {
                throw;
            }

            return userAccounts;
        }

        public async Task<UserAccounts> ChangePassword(UserAccounts userAccounts)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UserInformationChangePassword " +
                     "@id = {0}, @password = {1}, @ischanged = {2}, @isactive = {3}",
                      userAccounts.Id, userAccounts.Password, userAccounts.IsChanged, userAccounts.IsActive);

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
