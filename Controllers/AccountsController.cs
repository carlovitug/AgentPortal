using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        public IAccountService _accountService;
        public IAccountRepository _accountRepository;

        public AccountsController(IAccountService accountService, IAccountRepository accountRepository)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        //Login Function 
        [HttpPost("LoginAccount")]
        public async Task<IActionResult> LoginAccount([FromBody] Login login)
        {
            var loginResponse = await _accountService.LoginUser(login);
            if (loginResponse.Item1)
                return Ok(new { loginResponse.Item2 });
            else
                return BadRequest(new { message = "Invalid username or password." });
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LoggedOut([FromBody] int id)
        {
            var logoutResponse = await _accountRepository.LoggedOut(id);
            if (logoutResponse)
                return Ok(new { logoutResponse });
            else
                return BadRequest(new { message = "Invalid username or password." });
        }

        //Get User
        [HttpGet("GetUserDetails")]
        public async Task<ActionResult<IEnumerable<UserAccounts>>> GetUsers()
        {
            var userResponse = await _accountRepository.GetAllUsers();
            return userResponse;
        }

        //Register Function
        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount([FromBody] Register register)
        {
            var registerResponse = await _accountService.RegisterUser(register);
            if (!registerResponse.Item2)
                return Ok(new { registerResponse.Item1, registerResponse.Item2, registerResponse.Item3 });
            else
                return Ok(new { registerResponse.Item2 });
        }

        //Update User
        [HttpPost("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] UserAccounts userAccounts)
        {
            var userResponse = await _accountService.UpdateUser(userAccounts);
            return Ok(new { userResponse });
        }
        
        //Delete User
        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromBody] int userRequestID)
        {
            var userResponse = await _accountRepository.DeleteUser(userRequestID);
            return Ok(new { userResponse });
        }

        //Function for Changing Password
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserAccounts userAccounts)
        {
            var registerResponse = await _accountService.ChangePassword(userAccounts);
            return Ok(new { message = "Succesful!" });
        }

        //Function for Password Reset
        [HttpPost("PasswordReset")]
        public async Task<IActionResult> PasswordReset([FromBody] ChangePass email)
        {
            var passResetResponse = await _accountService.PasswordReset(email);
            if (passResetResponse == "Success")
                return Ok(true);
            else
                return BadRequest(new { message = passResetResponse });
        }
    }
}