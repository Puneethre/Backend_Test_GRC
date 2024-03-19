//using Authentication.Controllers;
using GRCServices.Data;
using GRCServices.Interfaces;
using GRCServices.Models;
using GRCServices.Dto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text.Json;

namespace GRCServices.Services
{
    public class LoginService : LoginInterface
    {
        private readonly GRCDbContext _context;
        private delegate string DelgSendEmail(string sSubj, string sBody, string toAddresses);
        private readonly DelgSendEmail? EmailHandler = null;
        private readonly IConfiguration _configuration;


        public LoginService(GRCDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            EmailHandler = EmailUtility.SendEmail;
        }

        public async Task<response> Authenticate(string email, string password)
        {
            var res = new response();

            try
            {
                StringBuilder sb = new StringBuilder();
                var User = GetUserByEmail(email);
                if (User == null || User.LoginEmailId != email)
                {
                    res.Message = "Credentials are invalid";
                    return res;
                }

                if (User.Status == "Inactive")
                {
                    res.Message = "Your Account is Inactive, Please contact System Admin";
                    return res;
                }

                bool verifiedPassKey = VerifyPassword(password, User.Pwd);

                if (User != null && !(verifiedPassKey))
                {
                    IncrementLoginAttempts(email);

                    if (User.NoofloginAttempts == 1)
                    {
                        res.Message = "Credentials are invalid .You are left with 1 more attempts";
                        return res;
                    }

                    if (User.NoofloginAttempts >= 2)
                    {
                        var tobeStatusUpdated = _context.SysUserLogins.Where(u => u.LoginEmailId == email && u.Status == "Active").FirstOrDefault();
                        tobeStatusUpdated.Status = "Inactive";
                        //tobeStatusUpdated.AccountLockStatus = true;
                        //tobeStatusUpdated.AccountLockExpirationDate = DateTime.UtcNow;
                        _context.SysUserLogins.Update(tobeStatusUpdated);
                        _context.SaveChanges();

                        res.Message = "You have been locked out for the day because of three invalid attempts during the day.";
                        return res;
                    }
                    res.Message = "Credentials are invalid";
                    return res;
                }

                ///Send SMS To Phone.....
                //SendSmsCode(user.PhoneNo, otpCode);

                var otpCode = GenerateOTP(); // Function to generate a random 6-digit code

                var userDetail = _context.SysUserLogins.Where(u => u.LoginEmailId == email).FirstOrDefault();
                userDetail.Mfacode = otpCode.ToString();
                //userDetail.LastLoginDatetime = DateTime.UtcNow;
                _context.SysUserLogins.Update(userDetail);
                _context.SaveChanges();


                string uid = "8C644244-FC1F-4A84-B7E0-11344646BB25";
                ///////Sending Email-------///////////
                string resetPasswordUrl = $"http://localhost:3000/resetpassword?UID={uid}";
                //string resetPasswordUrl = $"http://192.168.29.192:4200/reset?UID={uid}";
                var Subject = _configuration.GetSection(key: "EmailBodyForMFA:Subject").Value;
                var EmailBody = _configuration.GetSection(key: "EmailBodyForMFA:Body").Value;
                string subject = string.Format($"{Subject}");
                string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
                string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>"+
                        $"<h1>Dear {User.Name}</h1>"+$"{otpCode}"+ $"Click the link below to reset your password:<br><a href='{resetPasswordUrl}'>{resetPasswordUrl}</a>"+
                $"{EmailBody}</html>";
                

                //$"Click the link below to reset your password:<br><a href='{resetPasswordUrl}'>Reset Password</a>";
                sb.Append(sBody);
               sb.Append(strFooter);
               EmailHandler(subject, sb.ToString(), User.LoginEmailId);

               res.userid = User.SysUserId;
               res.Message = " MFA code sent successfully";
               ResetLoginAttempts(email);
               return res;
            }
            catch(Exception ex)
            {
                res.Message = ex.InnerException.Message;
                return res;
            }
        } 
        public async Task<response> ResetPassword(string email, string oldPassword, string newPassword)
        {
            var res = new response();
            try
            {
                if (email == null || oldPassword == null || newPassword == null)
                {
                    res.Message = "Please fill the Username and OldPassword";
                    return res;
                }

                StringBuilder sb = new StringBuilder();
                var User = GetUserByEmail(email);
                bool verifiedPassKey = VerifyPassword(oldPassword, User.Pwd);

                if (!verifiedPassKey)
                {
                    res.Message = "Your credentials are Invalid, Please try again!."; ;
                    return res;
                }
                else
                {
                    var statusToBeChanged = _context.SysUserLogins.Where(u => u.LoginEmailId == email && u.Status == "Active").FirstOrDefault();
                    //statusToBeChanged.ChangePasswordFlag = false;
                    statusToBeChanged.Pwd = HashPassword(newPassword);
                    //statusToBeChanged.LastPasswordChangeDate = DateTime.UtcNow;
                    //statusToBeChanged.uid = null;
                    _context.SaveChanges();
                }
                var Subject = _configuration.GetSection(key: "EmailBodyForpwdReset:Subject").Value;
                var EmailBody = _configuration.GetSection(key: "EmailBodyForpwdReset:Body").Value;
                string subject = string.Format($"{Subject}");
                string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
                string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>" +
                        $"<h1>Dear {User.Name}</h1>" +
                       $"{EmailBody}</html>";

                sb.Append(sBody);
                sb.Append(strFooter);
                EmailHandler(subject, sb.ToString(), User.LoginEmailId);

                res.Message = "You Password has been Changed Please Login Again";
                return res;
            }
            catch(Exception ex)
            {
                res.Message = ex.InnerException.Message;
                return res;
            }
        }

        public async Task<response> ForgotPassword(string email, string newPassword)
        {
            var res = new response();
            StringBuilder sb = new StringBuilder();
            var User = GetUserByEmail(email);

            var passwordToBeChanged = _context.SysUserLogins.Where(u => u.LoginEmailId == email && u.Status == "Active").FirstOrDefault();
            passwordToBeChanged.Pwd = HashPassword(newPassword);
            //passwordToBeChanged.uid = null;
            var Subject = _configuration.GetSection(key: "EmailBodyForpwdReset:Subject").Value;
            var EmailBody = _configuration.GetSection(key: "EmailBodyForpwdReset:Body").Value;
            string subject = string.Format($"{Subject}");
            string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
            string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>" +
                    $"<h1>Dear {User.Name}</h1>" +
                   $"{EmailBody}</html>";

            sb.Append(sBody);
            sb.Append(strFooter);
            EmailHandler(subject, sb.ToString(), User.LoginEmailId);

            res.Message = "Password Changed Successfully ...Please Login again...!";
            return res;
        }

        public async Task<response> SendMFAForForgotPWD(string email)
        {
            var res = new response();
            StringBuilder sb = new StringBuilder();
            var User = GetUserByEmail(email);
            if(User ==null)
            {
                res.Message = "User was Not Found....!";
            }

            var otpCode = GenerateOTP(); // Function to generate a random 6-digit code

            var userDetail = _context.SysUserLogins.Where(u => u.LoginEmailId == email).FirstOrDefault();
            userDetail.Mfacode = otpCode.ToString();
           // userDetail.LastLoginDatetime = DateTime.UtcNow;
            _context.SysUserLogins.Update(userDetail);
            _context.SaveChanges();

            var Subject = _configuration.GetSection(key: "EmailBodyForpwdReset:Subject").Value;
            var EmailBody = _configuration.GetSection(key: "EmailBodyForpwdReset:Body").Value;
            string subject = string.Format($"{Subject}");
            string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
            string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>" +
                    $"<h1>Dear {User.Name}</h1>" + $"{otpCode}" +
                    $"{EmailBody}</html>";

            sb.Append(sBody);
            sb.Append(strFooter);
            EmailHandler(subject, sb.ToString(), User.LoginEmailId);

            res.userid = User.SysUserId;
            res.Message = "Please Complete your Process with MFACode...!";
            return res;
        }

        public uidresponse isUIDExist(string uid)
        {
            var res = new uidresponse();
            if (uid == null)
            {
                res.isUiDExist = false;
                return res;
            }
            string isexist = _context.SysUserLogins.Where(u => u.Name == uid).Select(c => c.Name).FirstOrDefault();
            if (isexist != null)
            {
                res.isUiDExist = true;
                return res;
            }
            res.isUiDExist = false;
            return res;
        }

        public async Task<response> VerifyOtp(int userid,string code)
        {
            var res = new response();
            var user = GetUserByuserid(userid);
            if (user == null)
            {
                res.Message = "User not found";
                return res;
            }

            if (code == user.Mfacode)
            {
                res.Message = $"This User Was Valid...!";
                return res;
            }
            else
            {
                res.Message = "Invalid MFA code";
                return res;
            }
        }

            public void IncrementLoginAttempts(string email)
        {
            // Retrieve user from the database
            var user = _context.SysUserLogins.FirstOrDefault(u => u.LoginEmailId == email);
            if (user != null)
            {

                user.NoofloginAttempts = user.NoofloginAttempts + 1;

                _context.SaveChanges();
            }
        }

        public void ResetLoginAttempts(string email)
        {
            var user = _context.SysUserLogins.FirstOrDefault(u => u.LoginEmailId == email);
            if (user != null && user.Status == "Active")
            {
                user.NoofloginAttempts = 0;
                _context.SaveChanges();
            }
        }

        private string GenerateJwtToken(string name,string? role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                   issuer: _configuration["JWT:ValidIssuer"],
                   audience: _configuration["JWT:ValidAudience"],
                   expires: DateTime.Now.AddHours(3),
                   claims: claims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );
            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string password)
        {
            // Generate a salt and hash the password using bcrypt
            string salt = BCryptNet.GenerateSalt();
            string hashedPassword = BCryptNet.HashPassword(password, salt);
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the entered password against the hashed password
            return BCryptNet.Verify(password, hashedPassword);
        }

        public validationresponse ValidateMFA(int userid, string code)
        {
            var res = new validationresponse();
            Guid newGuid = Guid.NewGuid();
            var user = GetUserByuserid(userid);
            if (user == null)
            {
                res.Message = "User not found";
                return res;
            }

            if (code == user.Mfacode)
            {
                res.username = user.Name;
                res.UserId = user.SysUserId;
                user.LastloginDatetime = DateOnly.FromDateTime(DateTime.UtcNow);
               // var roleid = _context.UserRoles.Where(u => u.UserId == user.UserId).Select(u => u.RoleId).FirstOrDefault();  
                //var rolename = _context.Roles.Where(s => s.RoleId == roleid).Select(s => s.RoleName).FirstOrDefault();
                res.AuthenticationToken = newGuid.ToString();
                //res.AuthenticationToken = GenerateJwtToken(user.Username, rolename);
                res.Message = $" User Authenticated Successfully...!";
                int? roletype = _context.SysRoleMasters.Where(c => c.SysRoleId == user.SysRoleId).Select(c => c.RoleTypeId).FirstOrDefault();
                res.isSystemAdmin = roletype == 0 ? true : false;
                res.isClientAdmin = roletype == 1 ? true : false;
                res.sysRoleId = res.isSystemAdmin ? user.SysRoleId : null;
                return res;
            }
            else
            {
                res.Message = "Invalid MFA code";
                return res;
            }
        }

        public SysUserLogin GetUserByEmail(string? email)
        {
            SysUserLogin sult = new SysUserLogin();
            var user = _context.SysUserLogins.Where(u => u.LoginEmailId == email).FirstOrDefault();
            if(user == null)
            {
                return sult;
            }
            sult.LoginUserId = user.LoginUserId;
            sult.SysUserId = user.SysUserId;
            sult.Name = user.Name;
            sult.Status = user.Status;
            sult.Pwd = user.Pwd;
            sult.LoginEmailId = user.LoginEmailId;
            //sult.AccountLockExpirationDate = user.AccountLockExpirationDate;
            //sult.AccountLockStatus = user.AccountLockStatus;
            //sult.LastPasswordChangeDate = user.LastPasswordChangeDate;
            sult.NoofloginAttempts = user.NoofloginAttempts;
            sult.LastloginDatetime = user.LastloginDatetime;
            sult.Mfacode = user.Mfacode;
            sult.Phoneno = user.Phoneno;
            sult.SysRoleId = user.SysRoleId;
            sult.Uid = user.Uid;
            return sult;
        }

        public SysUserLogin GetUserByuserid(int? userid)
        {
            SysUserLogin sult = new SysUserLogin();
            var user = _context.SysUserLogins.Where(u =>u.SysUserId == userid).FirstOrDefault();
            sult.LoginUserId = user.LoginUserId;
            sult.SysUserId= user.SysUserId;
            sult.Name = user.Name;
            sult.Pwd = user.Pwd;
            sult.LoginEmailId = user.LoginEmailId;
            sult.Mfacode = user.Mfacode;
            sult.LastloginDatetime = user.LastloginDatetime;
            sult.Phoneno = user.Phoneno;
            sult.SysRoleId = user.SysRoleId;
            return sult;
        }

        public int GenerateOTP()
        {
            Random random = new Random();
            int otpNumber = random.Next(100000, 999999); // Generates a number between 100,000 and 999,999 (6 digits)
            return otpNumber;
        }
    }
}
