using Core.Model.Authentication;
using Core.Services;
using Core.Settings;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRWebApp.Service.Concretes
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings appSettings;
        public JwtService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        public string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string ValidateUser(string token)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            string userID = null;
            SecurityToken securityToken = null;
            try
            {
                //Check if token is valid(expire time etc.)
                tokenHandler.ValidateToken(token, new TokenValidationParameters { ValidateLifetime = true, IssuerSigningKey = new SymmetricSecurityKey(key), ValidateAudience = false, ValidateIssuer = false }, out securityToken);
                if (securityToken != null)
                {
                    userID = ((JwtSecurityToken)securityToken).Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

                }
                return userID;

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
