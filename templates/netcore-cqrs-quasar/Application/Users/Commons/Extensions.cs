using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Persistence.Identity;
using Domain.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commons
{
    public static class Extensions
    {

        public static async Task<string> GenerateJWtToken<T>(this UserManager<ApplicationUser> _userManager, ILogger<T> logger, ApplicationUser user, AppConfig appConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appConfig.Secret);
            var userClaims = await _userManager.GetUserClaims(user.Id);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims.AsEnumerable()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            logger.LogInformation($"GENERATED TOKEN: {tokenHandler.WriteToken(token)}");
            return tokenHandler.WriteToken(token);
        }

        public static async Task<List<Claim>> GetUserClaims(this UserManager<ApplicationUser> _userManager, String id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return new List<Claim>();

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            return authClaims;
        }

        public static async Task<ApplicationUser> Authenticate<T>(this UserManager<ApplicationUser> userManager, ILogger<T> logger, SignInManager<ApplicationUser> signInManager, AppConfig appConfig, string Username, string Password)
        {
            var loginRes = await signInManager.PasswordSignInAsync(Username, Password, false, lockoutOnFailure: false);
            if (!loginRes.Succeeded)
            {
                throw new InvalidOperationException("username/password combination does not match!");

            }
            var user = await userManager.FindByNameAsync(Username);
            if (user == null) return null;
            user.Token = await userManager.GenerateJWtToken<T>(logger, user, appConfig);
            user.Role = String.Join("", await userManager.GetRolesAsync(user));

            return user;
        }

        public static async Task<IdentityResult> ResetPassword(this UserManager<ApplicationUser> userManager, string username, string newPassword)
        {
            var user = await userManager.FindByNameAsync(username);

            // User exists return.
            if (user == null) throw new InvalidOperationException();
            // user is not new, password has changed in active directory. Update their password 
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }

    }
}