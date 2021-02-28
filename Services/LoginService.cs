using DemoMiddleware.Common;
using DemoMiddleware.IServices;
using DemoMiddleware.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace DemoMiddleware.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object Loginsync(AppUser user)
        {

            var roles = "User"; // await _userManager.GetRolesAsync(user);
            var permissions = "Read"; // await GetPermissionByUserId(user.Id.ToString());
            var claims = new[]
            {
                    new Claim("Email", user.Email),
                    new Claim(SystemConstants.UserClaim.Id, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(SystemConstants.UserClaim.FullName, user.FullName??string.Empty),
                    new Claim(SystemConstants.UserClaim.Roles, string.Join(";", roles)),
                    new Claim(SystemConstants.UserClaim.Permissions, JsonConvert.SerializeObject(permissions)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                 claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);
            return (new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
