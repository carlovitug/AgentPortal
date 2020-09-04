using ABMS_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAccountService
    {
        Task<Tuple<bool, UserAccounts>> LoginUser(Login login);
        Task<Tuple<UserAccounts, bool, bool>> RegisterUser(Register register);
        Task<UserAccounts> UpdateUser(UserAccounts userAccounts);
        Task<UserAccounts> ChangePassword(UserAccounts userAccounts);
        Task<string> PasswordReset(ChangePass email);
    }
}
