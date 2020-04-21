using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //Password Encryption and checking the Login Credentials 
        public async Task<Tuple<bool, UserAccounts>> LoginUser(Login login)
        {
            login.Password = EncryptPassword(login.Password);

            //Check if User Exists
            var roles = await _accountRepository.GetUser(login);

            if (roles.Item1)
            {
                return Tuple.Create(roles.Item1, roles.Item2);
            }
            else
            {
                return Tuple.Create(roles.Item1, roles.Item2);
            }
        }

        //Password Encryption and register the user credentials
        public async Task<Tuple<UserAccounts, bool>> RegisterUser(Register register)
        {
            register.Password = EncryptPassword(register.Password);

            UserAccounts userAccounts = new UserAccounts()
            {
                ApplicationId = register.ApplicationId,
                FullName = register.FullName,
                Email = register.Email,
                Username = register.Username,
                Password = register.Password,
                IsActive = true,
                IsChanged = false,
                RoleID = register.RoleID,
                IsDeleted = false
            };

            //check if the user and email exists 
            var isUserExists = await _accountRepository.CheckExistingUser(userAccounts);
            var isEmailExists = await _accountRepository.CheckExistingEmail(userAccounts.Email);

            if (!isUserExists && !isEmailExists)
            {
                var registerUser = await _accountRepository.RegisterUser(userAccounts);
                return Tuple.Create(registerUser, isUserExists);
            }
            else
            {
                return Tuple.Create(userAccounts, isUserExists);
            }
        }

        public async Task<UserAccounts> UpdateUser(UserAccounts userAccounts)
        {
            userAccounts.Password = EncryptPassword(userAccounts.Password);
            UserAccounts userAccountslst = new UserAccounts()
            {
                Id = userAccounts.Id,
                FullName = userAccounts.FullName,
                Email = userAccounts.Email,
                Username = userAccounts.Username,
                Password = userAccounts.Password,
                IsActive = true,
                IsChanged = false,
                RoleID = userAccounts.RoleID,
                IsDeleted = false
            };

            var userResponse = await _accountRepository.UpdateUser(userAccountslst);
            return userResponse;
        }

        //changing the user password
        public async Task<UserAccounts> ChangePassword(UserAccounts userAccounts)
        {
            userAccounts.Password = EncryptPassword(userAccounts.Password);
            userAccounts.IsActive = true;

            var changePassResponse = await _accountRepository.UpdateUser(userAccounts);
            return changePassResponse;
        }

        //generate new password and send it to user's email
        public async Task<bool> PasswordReset(ChangePass email)
        {
            var isUserExists = await _accountRepository.CheckExistingEmail(email.Email);
            if (isUserExists)
            {
                string generatedPass = CreateRandomPassword();
                string encryptPass = EncryptPassword(generatedPass);

                UserAccounts userAccounts = new UserAccounts
                {
                    Id = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item7,
                    Username = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item3,
                    Email = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item1,
                    FullName = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item2,
                    RoleID = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item4,
                    IsActive = (await _accountRepository.GetUserDetailsForPasswordReset(email)).Item5,
                    IsChanged = true,
                    Password = encryptPass,
                    IsDeleted = false
                };

                bool emailResponse = SendNewPasswordToEmail(email.Email, generatedPass, userAccounts.Username);

                await _accountRepository.UpdateUser(userAccounts);
                return emailResponse;
            }
            else
            {
                return false;
            }
        }

        public static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string specialChar = "!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (i != 7)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }
                else
                {
                    chars[i] = specialChar[random.Next(0, specialChar.Length)];
                }
            }

            return new string(chars);
        }

        public static bool SendNewPasswordToEmail(string email, string generatedPass, string username)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mobi.autogenerated@gmail.com");
                mail.To.Add(new MailAddress(email));
                mail.Subject = "Password Reset";
                mail.IsBodyHtml = true;
                mail.Body = $"Hello <b>{username}</b>, <br><br>We wanted to let you know that your account password was reset.<br><br>" +
                    $"This is your new password: <ul> {generatedPass} </ul>";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mobi.autogenerated@gmail.com", "mobi.autogenerated123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string EncryptPassword(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));

            //get hash result after compute it  
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            pass = strBuilder.ToString();
            return pass;
        }
    }
}
