using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAccountRepository
    {
        Task<Tuple<bool, UserAccounts>> GetUser(Login login);
        Task<ActionResult<IEnumerable<UserAccounts>>> GetAllUsers(int applicationId);
        Task<string> GetUserDetails(int id);
        Task<UserAccounts> RegisterUser(UserAccounts userAccounts);
        Task<UserAccounts> UpdateUser(UserAccounts userAccounts);
        Task<int> DeleteUser(int userAccountsID);
        Task<bool> CheckExistingUser(UserAccounts userAccounts);
        Task<bool> CheckExistingEmail(string email);
        Task<UserAccounts> ChangePasswordAccount(UserAccounts userAccounts);
        Task<Tuple<string, string, string, int, bool, bool, int>> GetUserDetailsForPasswordReset(ChangePass email);
    }
}
